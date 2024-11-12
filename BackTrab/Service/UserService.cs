using static TrabalhoBackEnd.Models.Users;
using TrabalhoBackEnd.Errors;
using TrabalhoBackEnd.Models.Responses;
using TrabalhoBackEnd.Models;
using TrabalhoBackEnd.IDAL;
using TrabalhoBackEnd.ISecurity;

namespace TrabalhoBackEnd.BLL
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IDescriptionRepository _descriptionRepository;
        private readonly IJwtService _jwt; 
        private readonly ILogger<UserService> _log;

        public UserService(IUserRepository userRepository, IDescriptionRepository descriptionRepository, IJwtService jwt, ILogger<UserService> log)
        {
            _userRepository = userRepository;
            _descriptionRepository = descriptionRepository;
            _jwt = jwt;
            _log = log;
        }

        public User Insert(User user)
        {
            if (user.Id != 0)
                throw new ArgumentException("Usuário já inserido!");

            if (_userRepository.FindByEmail(user.Email) != null)
                throw new BadRequestException($"Usuário com email {user.Email} já existe!");

            return _userRepository.Save(user);
        }

        public List<User> List(string role)
        {
            if (!string.IsNullOrEmpty(role))
            {
                return _userRepository.FindByRole(role);
            }
            else
            {
                    throw new BadRequestException("Invalido!");
            }
        }
        public User Update(long id, string name)
        {
            var user = _userRepository.FindById(id);
            if (user == null)
                throw new NotFoundException($"Usuário {id} não encontrado!");

            if (user.Name == name)
                return null;

            user.Name = name;
            return _userRepository.Save(user);
        }


        public User FindByIdOrNull(long id)
        {
            return _userRepository.FindById(id);
        }

        public User Delete(long id)
        {
            var user = _userRepository.FindById(id);
            if (user == null)
                return null;

            if (user.Roles.Any(r => r.Name == "ADMIN") &&
                _userRepository.FindByRole("ADMIN").Count == 1)
            {
                throw new BadRequestException("Não é possível excluir o último administrador!");
            }

            _userRepository.Delete(user);
            return user;
        }

        public bool AddRole(long id, string roleName)
        {
            var user = _userRepository.FindById(id);
            if (user == null)
                throw new NotFoundException("Usuário não encontrado");

            if (user.Roles.Any(r => r.Name == roleName))
                return false;

            var role = _descriptionRepository.FindByName(roleName);
            if (role == null)
                throw new BadRequestException("descrição Invalida!");

            user.Roles.Add(role);
            _userRepository.Save(user);
            return true;
        }

        public LoginResponse Login(string email, string password)
        {
            var user = _userRepository.FindByEmail(email);
            if (user == null || user.Password != password)
                return null;

            _log.LogInformation("User logged in. id={Id} name={Name}", user.Id, user.Name);
            return new LoginResponse
            {
                Token = _jwt.CreateToken(user),
                User = new UserResponse(user)
            };
        }
    }
}

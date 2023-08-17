using MusicApp.Models.Entities;

namespace MusicApp.Models
{
    public class DbInitializer
    {
        public static void Initialize(BlogContext context)
        {
            if(!context.Users.Any())
            {
                var clients = new User[]
                {
                      new User { Email = "prueba@gmail.com", FirstName="Prueba", LastName="Prueba", Password="123456"},
                      new User { Email = "juanito@gmail.com", FirstName = "Juanito", LastName = "González", Password = "qwerty123" },
                      new User { Email = "laura@example.com", FirstName = "Laura", LastName = "Martínez", Password = "myp@ssw0rd" },
                      new User { Email = "carlos23@hotmail.com", FirstName = "Carlos", LastName = "López", Password = "abcde987" },
                      new User { Email = "maria.smith@yahoo.com", FirstName = "María", LastName = "Smith", Password = "pass1234" },
                      new User { Email = "jose89@gmail.com", FirstName = "José", LastName = "Ramírez", Password = "hola12345" }
                };
                foreach (User user in clients)
                {
                    context.Users.Add(user);
                }
                //guardamos todo
                context.SaveChanges();
            }
        }
    }
}

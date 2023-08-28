using Microsoft.Extensions.Hosting;
using MusicApp.Models.Entities;
using MusicApp.Models.Enums;
using System;

namespace MusicApp.Models
{
    public class DbInitializer
    {
        public static void Initialize(BlogContext context)
        {
            if(!context.Users.Any())
            {
                context.AddRange(new User[]
                {
                      new User { Email = "prueba@gmail.com", FirstName="Prueba", LastName="Prueba", Password="123456"},
                      new User { Email = "juanito@gmail.com", FirstName = "Juanito", LastName = "González", Password = "qwerty123" },
                      new User { Email = "laura@example.com", FirstName = "Laura", LastName = "Martínez", Password = "myp@ssw0rd" },
                      new User { Email = "carlos23@hotmail.com", FirstName = "Carlos", LastName = "López", Password = "abcde987" },
                      new User { Email = "maria.smith@yahoo.com", FirstName = "María", LastName = "Smith", Password = "pass1234" },
                      new User { Email = "jose89@gmail.com", FirstName = "José", LastName = "Ramírez", Password = "hola12345" }
                });
            }

            if(!context.Posts.Any())
            {
                var user = context.Users.FirstOrDefault(u => u.Email == "juanito@gmail.com");
                if (user != null)
                {
                    context.AddRange(new Post[]
                    {
                         new Post { CreationDate = DateTime.Now.AddDays(-5), Title = "Descubriendo el mundo del Rock", Image = "", Text = "En esta publicación, exploraremos la historia y la influencia del género del Rock en la industria de la música. Desde sus raíces en los años 50 hasta las leyendas del Rock como The Beatles y Led Zeppelin.", Category = Category.MUSIC_HISTORY.ToString(), UserId = user.Id },
                         new Post { CreationDate = DateTime.Now.AddDays(-3), Title = "Los secretos detrás de la música electrónica", Image = "", Text = "La música electrónica ha revolucionado la forma en que experimentamos el sonido. Desde los inicios del sintetizador hasta los festivales de música electrónica de hoy en día, exploraremos cómo este género ha evolucionado a lo largo de los años.", Category = Category.MUSIC_GENRES.ToString(), UserId = user.Id },
                         new Post { CreationDate = DateTime.Now.AddDays(-2), Title = "Explorando la diversidad del Pop", Image = "", Text = "El género del Pop abarca una amplia gama de estilos y artistas. En esta publicación, analizaremos las tendencias actuales en la música Pop y destacaremos algunos de los artistas más influyentes en este género.", Category = Category.STORIES_OF_BANDS_AND_FAMOUS_ARTISTS.ToString(), UserId = user.Id },
                         new Post { CreationDate = DateTime.Now.AddDays(-1), Title = "El impacto del Hip Hop en la cultura moderna", Image = "", Text = "El Hip Hop ha trascendido las fronteras musicales y se ha convertido en un movimiento cultural global. Examina cómo el Hip Hop ha influido en la moda, el arte y la sociedad en general, desde sus orígenes en las calles de Nueva York.", Category = Category.MUSIC_GENRES.ToString(), UserId = user.Id },
                         new Post { CreationDate = DateTime.Now.AddDays(-1), Title = "La magia de la música clásica", Image = "", Text = "La música clásica nos transporta a través del tiempo y nos conecta con las composiciones atemporales de maestros como Beethoven, Mozart y Bach. Explora las emociones y la belleza que residen en la música clásica.", Category = Category.MUSIC_HISTORY.ToString(), UserId = user.Id}
                    });
                }
            }          

            context.SaveChanges();
        }
    }
}

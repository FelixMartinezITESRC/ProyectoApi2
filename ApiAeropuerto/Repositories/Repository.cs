using Microsoft.EntityFrameworkCore;

namespace ApiAeropuerto.Repositories
{
    public class Repository<T> where T : class
    {
        private readonly DbContext context;

        public Repository(DbContext context)
        {
            this.context = context;
        }

        public DbSet<T> Get() //Obtiene todos los elementos.
        {
            return context.Set<T>();
        }

        public T? Get(object v) //Busca uno en especifico.
        {
            return context.Find<T>(v);
        }

        public void Insert(T entidad) //Inserta uno.
        {
            context.Add(entidad);
            context.SaveChanges();
        }

        public void Update(T entidad) //Actualiza uno.
        {
            context.Update(entidad);
            context.SaveChanges();
        }

        public void Delete(T entidad) //Elimina uno.
        {
            context.Remove(entidad);
            context.SaveChanges();
        }
    }
}

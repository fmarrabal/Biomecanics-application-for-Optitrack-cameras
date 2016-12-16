using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kibiomer_app.Data
{
    public class marcadoresOrdenes
    {
        public static List<Marcadores> ObtenerMarcadores()
        { 
            using (kibiomerEntities db = new kibiomerEntities())
            {
                return db.Marcadores.ToList();
            }
        }
        public static bool InsertMarcadores(Marcadores obj)
        {
            try
            {
                using (kibiomerEntities db = new kibiomerEntities())
                {
                    db.Marcadores.Add(obj);
                    db.SaveChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }

    public class numeroserie
    {
        public static List<NoSeries> ObtenerNoSeries()
        {
            using (kibiomerEntities db = new kibiomerEntities())
            {
                return db.NoSeries.ToList();
            }
        }
        public static bool InsertNoSeries(NoSeries obj)
        {
            try
            {
                using (kibiomerEntities db = new kibiomerEntities())
                {
                    db.NoSeries.Add(obj);
                    db.SaveChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}

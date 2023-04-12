using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Zona
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.UestebanCineContext context = new DL.UestebanCineContext())
                {
                    var query = context.Zonas.FromSqlRaw("ZonaGetAll");
                    result.Objects = new List<object>();

                    if(query != null)
                    {
                        foreach (var item in query) 
                        {
                            ML.Zona zona = new ML.Zona();

                            zona.IdZona =item.IdZona;
                            zona.Descripcion = item.Descripcion;

                            result.Objects.Add(zona);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }
            catch(Exception e)
            {
                result.Correct = false;
                result.ErrorMessage = e.Message;
            }
            return result;
        }
    }
}

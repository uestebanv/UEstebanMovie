using Microsoft.EntityFrameworkCore;

namespace BL
{
    public class Cine
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.UestebanCineContext context = new DL.UestebanCineContext())
                {
                    var query = context.Cines.FromSqlRaw("CineGetAll").ToList();
                    result.Objects = new List<object>();

                    if (query != null)
                    {
                        foreach (var Item in query)
                        {
                            ML.Cine cine = new ML.Cine();

                            cine.IdCine = Item.IdCine;
                            cine.Latitud = Item.Latitud.Value;
                            cine.Longitud = Item.Longitud.Value;
                            cine.Direccion = Item.Direccion;
                            cine.Venta = Item.Venta.Value;

                            cine.Zona = new ML.Zona();
                            cine.Zona.IdZona = Item.IdZona.Value;
                            cine.Zona.Descripcion = Item.Descripcion;

                            result.Objects.Add(cine);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public static ML.Result GetById(int idCine)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.UestebanCineContext context = new DL.UestebanCineContext())
                {
                    var query = context.Cines.FromSqlRaw($"CineGetById '{idCine}'").AsEnumerable().FirstOrDefault();

                    if (query != null)
                    {
                        ML.Cine cine = new ML.Cine();

                        cine.IdCine = query.IdCine;
                        cine.Latitud = query.Latitud.Value;
                        cine.Longitud = query.Longitud.Value;
                        cine.Direccion = query.Direccion;
                        cine.Venta = query.Venta.Value;

                        cine.Zona = new ML.Zona();
                        cine.Zona.IdZona = query.IdZona.Value;
                        cine.Zona.Descripcion = query.Descripcion;

                        result.Object = cine;

                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public static ML.Result Add(ML.Cine cine)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.UestebanCineContext context = new DL.UestebanCineContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"CineAdd '{cine.Latitud}'," +
                                                                       $"'{cine.Longitud}'," +
                                                                       $"'{cine.Direccion}'," +
                                                                       $"'{cine.Venta}'," +
                                                                       $"'{cine.Zona.IdZona}'");
                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }


        public static ML.Result Update(ML.Cine cine)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.UestebanCineContext context = new DL.UestebanCineContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"CineUpdate '{cine.IdCine}'," +
                                                                          $"'{cine.Latitud}'," +
                                                                          $"'{cine.Longitud}'," +
                                                                          $"'{cine.Direccion}'," +
                                                                          $"'{cine.Venta}'," +
                                                                          $"'{cine.Zona.IdZona}'");

                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }


        public static ML.Result Delete(int idCine)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.UestebanCineContext context = new DL.UestebanCineContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"CineDelete '{idCine}'");
                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }
            catch (Exception e)
            {
                result.Correct = false;
                result.ErrorMessage = e.Message;
            }
            return result;
        }


        public static ML.Result Calculo()
        {
            ML.Zona zona = new ML.Zona();
            ML.Result result = new ML.Result();
            try
            {
                using (DL.UestebanCineContext context = new DL.UestebanCineContext())
                {
                    var query = context.Cines.FromSqlRaw("CineGetAll").ToList();
                    result.Objects= new List<object>();

                    if (query != null)
                    {
                        foreach (var Item in query)
                        {
                            zona.VentaTotal += Item.Venta.Value;

                            if (Item.IdZona == 1)
                            {
                                zona.ZCentro += Item.Venta.Value;   
                            }
                            if (Item.IdZona == 2)
                            {
                                zona.ZNorte += Item.Venta.Value;
                            }
                            if (Item.IdZona == 3)
                            {
                                zona.ZOriente += Item.Venta.Value;
                            }
                            if (Item.IdZona == 4)
                            {
                                zona.ZPoniente += Item.Venta.Value;
                            }
                            if (Item.IdZona == 5)
                            {
                                zona.ZSur += Item.Venta.Value;
                            }
                        }
                        zona.PCentro = (zona.ZCentro / zona.VentaTotal) * 100;
                        zona.PNorte = (zona.ZNorte / zona.VentaTotal) * 100;
                        zona.POriente = (zona.ZOriente / zona.VentaTotal) * 100;
                        zona.PPoniente = (zona.ZPoniente / zona.VentaTotal) * 100;
                        zona.PSur = (zona.ZSur / zona.VentaTotal) * 100;


                        result.Object = zona;

                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }
            catch (Exception ex)            
            {
            }
            return result;
        }
    }
}
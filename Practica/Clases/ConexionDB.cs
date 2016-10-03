using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using Practica;
using System.IO;

namespace Modelo.Clases
{
    public class ConexionDB
    {
        private SqlConnection conexion;
        private String datosConexion;

        public ConexionDB()
        {
            datosConexion = "Data Source=ronald-pc\\sqlexpress;Initial Catalog=practica;Integrated Security=True";
            conexion = new SqlConnection(datosConexion);    
        }

        public String actualizarPersona(Persona persona)
        {
            SqlCommand comandoInsertar = new SqlCommand();
            String resultado = null;
            int aux = 0;

            conexion.Open();
            comandoInsertar.Connection = conexion;
            comandoInsertar.CommandType = CommandType.StoredProcedure;
            comandoInsertar.CommandText = "actualizarPersona";
            comandoInsertar.Parameters.AddWithValue("@cedula", persona.getCedula());
            comandoInsertar.Parameters.AddWithValue("@nombre", persona.getNombre());
            comandoInsertar.Parameters.AddWithValue("@fechNac", String.Format("{0:yyyyMMdd}", persona.getFechaNac()));
            comandoInsertar.Parameters.AddWithValue("@sexo", persona.getSexo());
            comandoInsertar.Parameters.AddWithValue("@profesion", persona.getProfesion());
            comandoInsertar.Parameters.AddWithValue("@telefono", persona.getTelefono());
            comandoInsertar.Parameters.AddWithValue("@foto", persona.getFoto());
            try
            {
                aux = comandoInsertar.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                resultado = e.Message;

            }
            conexion.Close();
            return resultado;
        }

        public String insertarPersona(Persona persona)
        {
            SqlCommand comandoInsertar = new SqlCommand();
            String resultado = null;

            conexion.Open();
            comandoInsertar.Connection = conexion;
            comandoInsertar.CommandType = CommandType.StoredProcedure;
            comandoInsertar.CommandText = "insertarPersona";
            comandoInsertar.Parameters.AddWithValue("@cedula", persona.getCedula());
            comandoInsertar.Parameters.AddWithValue("@nombre", persona.getNombre());
            comandoInsertar.Parameters.AddWithValue("@fechNac", String.Format("{0:yyyyMMdd}", persona.getFechaNac()));
            comandoInsertar.Parameters.AddWithValue("@sexo", persona.getSexo());
            comandoInsertar.Parameters.AddWithValue("@profesion", persona.getProfesion());
            comandoInsertar.Parameters.AddWithValue("@telefono", persona.getTelefono());
            comandoInsertar.Parameters.AddWithValue("@foto", persona.getFoto());
            try
            {
                comandoInsertar.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                resultado = e.Message;
                
            }
            conexion.Close();
            return resultado;           
        }

        public bool buscarPersona(Persona persona)
        {
            SqlCommand comandoBuscar = new SqlCommand();
            SqlDataAdapter adaptador;
            DataTable tabla;

            conexion.Open();
            comandoBuscar.Connection = conexion;
            comandoBuscar.CommandType = CommandType.StoredProcedure;
            comandoBuscar.CommandText = "buscarPersona";
            comandoBuscar.Parameters.AddWithValue("@cedula", persona.getCedula());
            adaptador = new SqlDataAdapter(comandoBuscar);
            tabla = new DataTable();
            adaptador.Fill(tabla);
            conexion.Close();
            if ( tabla.Rows.Count != 0 )
            {

                foreach (DataRow objFila in tabla.Rows)
                {
                    persona.setNombre(objFila["nombre"].ToString());
                    persona.setFechaNac(DateTime.ParseExact(objFila["fechaNac"].ToString(),
                        "yyyyMMdd",System.Globalization.CultureInfo.InvariantCulture));
                    persona.setProfesion(objFila["profesion"].ToString());
                    persona.setTelefono(int.Parse(objFila["telefono"].ToString()));
                    persona.setSexo(char.Parse(objFila["sexo"].ToString()));
                    Byte[] data = new Byte[0];
                    data = (Byte[])(objFila["foto"]);
                    persona.setFoto(data);
                }

                return true;
            }

            return false;

        }

        public DataTable FiltrarPersonas(String filtrar)
        {
            SqlCommand comandoFiltrar = new SqlCommand();
            SqlDataAdapter adaptador;
            DataTable tabla;

            conexion.Open();
            comandoFiltrar.Connection = conexion;
            comandoFiltrar.CommandType = CommandType.StoredProcedure;
            comandoFiltrar.CommandText = "filtrarPersonas";
            comandoFiltrar.Parameters.AddWithValue("@filtro", filtrar);
            adaptador = new SqlDataAdapter(comandoFiltrar);
            tabla = new DataTable();
            adaptador.Fill(tabla);
            foreach (DataRow objFila in tabla.Rows)
                objFila["fechaNac"] = DateTime.ParseExact(objFila["fechaNac"].ToString(),
                        "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy");
            conexion.Close();

            return tabla;
        }

        public bool existePersona(int cedula)
        {

            SqlCommand comandoConsulta = new SqlCommand();
            bool existe = false;

            conexion.Open();
            comandoConsulta.Connection = conexion;
            comandoConsulta.CommandType = CommandType.StoredProcedure;
            comandoConsulta.CommandText = "existePersona";
            comandoConsulta.Parameters.AddWithValue("@cedula", cedula);

            comandoConsulta.Parameters.Add("@contador",SqlDbType.Int).Direction = ParameterDirection.Output;
            
            try
            {
                comandoConsulta.ExecuteNonQuery(); 
                if (int.Parse(comandoConsulta.Parameters["@contador"].Value.ToString()) > 0)
                    existe = true;
            }
            catch (Exception e)
            {
                throw e;
            }
            conexion.Close();
            return existe;
        }

        public bool eliminarPersona(int cedula)
        {
            SqlCommand comandoConsulta = new SqlCommand();
            bool eliminado = false;

            conexion.Open();
            comandoConsulta.Connection = conexion;
            comandoConsulta.CommandType = CommandType.StoredProcedure;
            comandoConsulta.CommandText = "eliminarPersona";
            comandoConsulta.Parameters.AddWithValue("@cedula", cedula);
            try
            {
                if (comandoConsulta.ExecuteNonQuery() > 0)
                    eliminado = true;
            }
            catch (Exception e)
            {
                throw e;
            }
            conexion.Close();
            return eliminado;
        }

    }

   
}

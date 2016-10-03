using Modelo.Clases;
using Practica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Control
{
    public class Controlador
    {
        private Persona persona;
        private ConexionDB conn;

        public Controlador()
        {
            persona = new Persona();
            conn = new ConexionDB();
        }

        public void setPersona(Persona persona)
        {
            this.persona = persona;
        }

        public Persona getPersona()
        {
            return persona;
        }

        public void setConexionDB(ConexionDB conn)
        {
            this.conn = conn;
        }

        public ConexionDB getConexionDB()
        {
            return conn;
        }

    }
}

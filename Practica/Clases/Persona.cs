using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica
{
    public class Persona
    {
        private int cedula;
        private String nombre;
        private int edad;
        private DateTime fechaNac;
        private Char sexo;
        private String profesion;
        private int telefono;
        private byte[] foto; 

        public Persona() { }
        public Persona(int cedula, String nombre, DateTime fechaNac, Char sexo, String profesion, int telefono, byte [] foto)
        {
            this.cedula = cedula;
            this.nombre = nombre;
            this.fechaNac = fechaNac;
            this.sexo = sexo;
            this.profesion = profesion;
            this.telefono = telefono;
            this.foto = foto;
            setEdad();
        }

        public void setCedula(int cedula)
        {
            this.cedula = cedula;
        }

        public void setNombre(String nombre)
        {
            this.nombre = nombre;
        }

        public void setEdad()
        {
            edad = DateTime.Today.Year - fechaNac.Year;
        }

        public void setFechaNac(DateTime fechaNac)
        {
            this.fechaNac = fechaNac;
        }

        public void setSexo(Char sexo)
        {
            this.sexo = sexo;
        }

        public void setProfesion(String profesion)
        {
            this.profesion = profesion;
        }

        public void setTelefono(int telefono)
        {
            this.telefono = telefono;
        }

        public void setFoto(byte [] foto)
        {
            this.foto = foto;
        }

        public int getCedula()
        {
            return cedula;
        }

        public String getNombre()
        {
            return nombre;
        }

        public int getEdad()
        {
            return edad;
        }

        public DateTime getFechaNac()
        {
            return fechaNac;
        }

        public Char getSexo()
        {
            return sexo;
        }

        public String getProfesion()
        {
            return profesion;
        }

        public int getTelefono()
        {
            return telefono;
        }

        public byte [] getFoto()
        {
            return foto;
        }
    }
}

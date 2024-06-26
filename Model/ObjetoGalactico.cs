using System;
using System.Globalization;

namespace Model
{
    public class ObjetoGalactico
    {
        private int id; //4 bytes
        private string tipo; // 10 bytes 
        private string nombre; // 14 bytes
        private DateTime descubrimiento; // 10 bytes 00/00/0000
        private double tamano; // 9 bytes
        private double distanciaTierra; // 9 bytes
        private bool agua; // 1 byte
        private bool vida; // 1 byte
        private bool atmosfera; // 1 byte
        // Total persona: 60 bytes (59 datos y 1 de activo/inactivo)

        public string Tipo { get => tipo; set => tipo = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public DateTime Descubrimiento { get => descubrimiento; set => descubrimiento = value; }
        public double Tamano { get => tamano; set => tamano = value; }
        public double DistanciaTierra { get => distanciaTierra; set => distanciaTierra = value; }
        public bool Agua { get => agua; set => agua = value; }
        public bool Vida { get => vida; set => vida = value; }
        public bool Atmosfera { get => atmosfera; set => atmosfera = value; }
        public int Id { get => id; set => id = value; }

        public ObjetoGalactico(string tipo, string nombre, DateTime descubrimiento, double tamano, double distanciaTierra, bool agua, bool vida, bool atmosfera)
        {
            this.id = -1;
            this.tipo = tipo;
            this.nombre = nombre;
            this.descubrimiento = descubrimiento;
            this.tamano = tamano;
            this.distanciaTierra = distanciaTierra;
            this.agua = agua;
            this.vida = vida;
            this.atmosfera = atmosfera;
        }

        public ObjetoGalactico(int id, string tipo, string nombre, DateTime descubrimiento, double tamano, double distanciaTierra, bool agua, bool vida, bool atmosfera)
            : this(tipo, nombre, descubrimiento, tamano, distanciaTierra, agua, vida, atmosfera)
        {
            this.id = id;
        }

        public ObjetoGalactico()
        {
            this.id = 0;
            this.tipo = "";
            this.nombre = "";
            this.descubrimiento = DateTime.MinValue;
            this.tamano = 0.0;
            this.distanciaTierra = 0.0;
            this.agua = false;
            this.vida = false;
            this.atmosfera = false;
        }

        public override string ToString()
        {

            return "[" + Id + "]->" + Nombre+" "+Descubrimiento;
        
        }

        public string FormatoCorrecto()
        {
            return this.id + "," + this.tipo + "," + this.nombre + "," + this.descubrimiento.ToString("dd/MM/yyyy") + "," + this.tamano.ToString(CultureInfo.InvariantCulture) + "," + this.distanciaTierra.ToString(CultureInfo.InvariantCulture) + "," + this.agua + "," + this.vida + "," + this.atmosfera;
        }

    }

    }

        

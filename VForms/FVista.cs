using BBusiness;
using Business;
using Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace VForms
{
    public partial class FVista : Form
    {

        // IBusiness business = new Business();
        BServAsync business = new BServAsync();
        ServidorHTTP servidor;


        public FVista()
        {
            InitializeComponent();
            servidor = new ServidorHTTP();
        }

        private void FVista_Load(object sender, EventArgs e)
        {
            lbLoading.Visible = true;
            picGif1.Visible = true;
            picGif2.Visible = true;

            ActualizarList();

        }

        private void btnAnadir_Click(object sender, EventArgs e)
        {
            lbLoading.Text = "Adding ...";
            lbLoading.Visible = true;
            picGif5.Visible = true;
            picGif6.Visible = true;
            FDetalles formDetalle = new FDetalles();
            formDetalle.ShowDialog();
            ActualizarList();


        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            if (lsbLista.SelectedItem != null)
            {
                lbLoading.Text = "Changing...";
                lbLoading.Visible = true;
                picGif3.Visible = true;
                picGif4.Visible = true;
                ObjetoGalactico og = (ObjetoGalactico)lsbLista.SelectedItem;
                FDetalles formDetalle = new FDetalles(og);
                formDetalle.ShowDialog();
                ActualizarList();
              
            }
            else
            {
                MessageBox.Show("No hay ningún elemento seleccionado.");
          
            }
          
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {

            DialogResult resultado;

            if (lsbLista.SelectedItem != null)
            {
                resultado = MessageBox.Show("¿Estás seguro de que quieres borrar este elemento?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (resultado == DialogResult.Yes)
            {
               
                    ObjetoGalactico og = (ObjetoGalactico)lsbLista.SelectedItem;
                    lbLoading.Text = "Deleting...";
                    lbLoading.Visible = true;
                    picGif5.Visible = true;
                    picGif6.Visible = true;
                    business.BorrarElemento(og.Id);
                    MessageBox.Show("Elemento eliminado.");

                }
                else
                {
                    MessageBox.Show("Operación cancelada");
                }
            }
            else
            {
                MessageBox.Show("No hay ningún elemento seleccionado.");
            }

            
            ActualizarList();
        }

        private void btnDetalle_Click(object sender, EventArgs e)
        {
            if (lsbLista.SelectedItem != null)
            {
                ObjetoGalactico og = (ObjetoGalactico)lsbLista.SelectedItem;
                FDetalles formDetalle = new FDetalles(og, false);
                formDetalle.ShowDialog();
                ActualizarList();
            }
            else
            {
                MessageBox.Show("No hay ningún elemento seleccionado.");
            }
        }

        private async void btnFecha_Click(object sender, EventArgs e)
        {

                DateTime fecha = dtBuscar.Value;
                lbLoading.Text = "Searching...";
                lbLoading.Visible = true;
                picGif1.Visible = true;
                picGif2.Visible = true;
                List <ObjetoGalactico> lista = await business.MostrarDesdeFecha(fecha);
                lsbLista.Items.Clear();
                lsbLista.Items.AddRange(lista.ToArray());
                lbLoading.Visible = false;
                picGif1.Visible = false;
                picGif2.Visible = false;

        }

        private async void btnAtmosfera_Click(object sender, EventArgs e)
        {
            lbLoading.Text = "Searching...";
            lbLoading.Visible = true;
            picGif1.Visible = true;
            picGif2.Visible = true;
            List<ObjetoGalactico> lista = await business.MostrarAtmosferas();
            lsbLista.Items.Clear();
            lsbLista.Items.AddRange(lista.ToArray());
            lbLoading.Visible = false;
            picGif1.Visible = false;
            picGif2.Visible = false;

        }

        private async void ActualizarList()
        {
            
            List<ObjetoGalactico> lista = await business.ListarElementos();
            lsbLista.Items.Clear();


            lsbLista.Items.AddRange(lista.ToArray());
            lbLoading.Visible = false;
            picGif1.Visible = false;
            picGif2.Visible = false;
            picGif3.Visible = false;
            picGif4.Visible = false;
            picGif5.Visible = false;
            picGif6.Visible = false;

        }

        private void txtActual_Click(object sender, EventArgs e)
        {
            lbLoading.Text = "Updating...";
            lbLoading.Visible = true;
            picGif3.Visible = true;
            picGif4.Visible = true;
            ActualizarList();
        }

        //Lanza o detiene el servidor y hace visible o invisible el text box que recibe las respuestas del cliente desde el navegador
        private void btServidor_Click(object sender, EventArgs e)
        {
            if (txtServidor.Visible == false)
            {
                txtServidor.Visible = true;
                this.Size = new Size(586, 671);
                toolServerStatus.Text = "Servidor arrancado";
                btServidor.BackColor = Color.Green;

                servidor.Arrancar();
                servidor.ActualizarTextBox += ActualizarTextBox;
            }
            else
            {
                txtServidor.Visible = false;
                this.Size = new Size(586, 552);
                servidor.Detener();
                toolServerStatus.Text = "Servidor parado";
                btServidor.BackColor = Color.MediumPurple;
            }
        }

        private void btBBDD_Click(object sender, EventArgs e)
        {

            List<String> mensajes = business.LanzarProcedimiento(); 

            if (mensajes.Count > 1)
            {
                MessageBox.Show(mensajes[0], "Respuesta del Procedimiento Almacenado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MessageBox.Show("Recuento de elementos en la tabla: " + mensajes[1], "Recuento de Elementos", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(mensajes[0], "Error en la petición", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }
        private void btnOut_Click(object sender, EventArgs e)
        {
            String cuerpo;
            String cabecera;
            MessageBoxIcon icono;
            DialogResult result;

            cuerpo = "¿Está seguro de que desea salir?";
            cabecera = "Cerrar sesión";
            icono = MessageBoxIcon.Information;
            MessageBoxButtons botones = MessageBoxButtons.OKCancel;
            result = MessageBox.Show(cuerpo, cabecera, botones, icono);

            if (result == DialogResult.OK)
            {
                this.Close();

            }
        }

        // Permite recibir mensajes desde ServidorHTTP
        private void ActualizarTextBox(string texto)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string>(ActualizarTextBox), texto);
            }
            else
            {
                txtServidor.Text += texto;
            }
        }

       
    }
  }
    
    


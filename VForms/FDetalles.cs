using BBusiness;
using Business;
using Model;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace VForms
{
    public partial class FDetalles : Form
    {
        ObjetoGalactico parametro;
        //IBusiness busness = new Business();
        BServAsync business = new BServAsync();
        

        bool mod;

        public FDetalles()
        {   
            
            InitializeComponent();
            dtDate.Value = DateTime.Now;    
            mod = true;
           


        }
        public FDetalles(ObjetoGalactico og)
        {
            InitializeComponent();

            txtId.Text = og.Id.ToString();
            txtTipo.Text = og.Tipo.ToString();
            txtNombre.Text = og.Nombre.ToString();
            dtDate.Value = og.Descubrimiento;
            txtSize.Value = ((decimal)og.Tamano);
            txtDistance.Value = ((decimal)og.DistanciaTierra);
            chbAgua.Checked = og.Agua;
            chbVida.Checked = og.Vida;
            chbAtmosf.Checked = og.Atmosfera;
            parametro= og;
            mod = true;
            txtUrl.Enabled = false;



        }

        public FDetalles(ObjetoGalactico og, bool modify)
        {
            InitializeComponent();

            txtId.Text = og.Id.ToString();
            txtTipo.Text = og.Tipo.ToString();
            txtTipo.Enabled = false;
            txtNombre.Text = og.Nombre.ToString();
            txtNombre.Enabled = false;
            dtDate.Value = og.Descubrimiento;
            dtDate.Enabled = false;
            txtSize.Value = (decimal)og.Tamano;
            txtSize.Enabled = false;
            txtDistance.Value = (decimal)og.DistanciaTierra;
            txtDistance.Enabled = false;
            chbAgua.Checked = og.Agua;
            chbAgua.Enabled = false;
            chbAgua.BackColor = Color.White;
            chbVida.Checked = og.Vida;
            chbVida.Enabled = false;
            chbVida.BackColor = Color.White;
            chbAtmosf.Checked = og.Atmosfera;
            chbAtmosf.Enabled = false;
            chbAtmosf.BackColor= Color.White;
            parametro = og;
            mod = modify;
            txtUrl.Enabled = false;

        }

        private async void txtAcept_Click(object sender, EventArgs e)
        {
            if (parametro == null || txtUrl.Enabled)
            {
                lbMensajes.Text = "Adding...";
                lbMensajes.Visible = true;
                ObjetoGalactico obj = new ObjetoGalactico(txtTipo.Text, txtNombre.Text, dtDate.Value, ((double)txtSize.Value), ((double)txtDistance.Value), chbAgua.Checked, chbVida.Checked, chbAtmosf.Checked);
                await business.AnadirElemento(obj);
                MessageBox.Show("Elemento añadido.");
                Close();
            }
            else if (mod == false)
            {

                Close();
            }else    
            {
                    lbMensajes.Text = "Changing...";
                    lbMensajes.Visible = true;
                    ObjetoGalactico obj = new ObjetoGalactico(parametro.Id, txtTipo.Text, txtNombre.Text, dtDate.Value, ((double)txtSize.Value), ((double)txtDistance.Value), chbAgua.Checked, chbVida.Checked, chbAtmosf.Checked);
                    await business.ModificarElemento(parametro.Id, obj);
                    MessageBox.Show("Elemento modificado.");
                    Close();
                
            }
        }

        private async void btPedir_Click(object sender, EventArgs e)
        {
            string url = txtUrl.Text;
            try
            {
                ObjetoGalactico og = await ScraperHTML.ImportDataAsync(url);

                txtId.Text = og.Id.ToString();
                txtTipo.Text = og.Tipo.ToString();
                txtNombre.Text = og.Nombre.ToString();
                dtDate.Value = og.Descubrimiento;
                txtSize.Value = ((decimal)og.Tamano);
                txtDistance.Value = ((decimal)og.DistanciaTierra);
                chbAgua.Checked = og.Agua;
                chbVida.Checked = og.Vida;
                chbAtmosf.Checked = og.Atmosfera;

                parametro = og;

           
                mod = false;
                txtId.Enabled = false;

                lbMensajes.Text = "Datos cargados con éxito.";
                lbMensajes.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al obtener los datos: {ex.Message}");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        
    }
}

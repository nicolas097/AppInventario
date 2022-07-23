using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppInventario
{
    public partial class InterfazSalida : Form
    {
        ServiceReference.WebServiceSoapClient auxServicio = new ServiceReference.WebServiceSoapClient();
        public InterfazSalida()
        {
            InitializeComponent();
            dateTimePicker.Enabled = false; 
            btnGuardar.Enabled = false; 
        }


        


        private void btnValidar_Click(object sender, EventArgs e)
        {

            ValidarStock();
        }

        private void Insertar()
        {
            ServiceReference.SalidaProducto auxSale = new ServiceReference.SalidaProducto();
            auxSale.numSalida = Convert.ToInt32(txtNumS.Text);
            auxSale.sku = txtSku.Text;
            auxSale.cantidad_salida = Convert.ToInt32(txtCantSal.Text);



            if (auxServicio.insertarService(auxSale))
            {
                auxServicio.serviceActualizaC(txtSku.Text, Convert.ToInt32(txtCantSal.Text));
                MessageBox.Show("Se agregarón los datos");
                btnGuardar.Enabled=false;   

            }
            else
            {
                MessageBox.Show("No se ha agregado nada");
            }
        }

        private bool ValidarStock()
        {
            if (Validacion())
            {
                if (Convert.ToInt32(txtCantSal.Text) > auxServicio.traerStock(txtSku.Text))
                {
                    MessageBox.Show("La cantidad a sacar es mayor a la que se encuentrad disponible");
                    
                }
                else
                {
                    MessageBox.Show("se encuentra la cantidad, deseada puede sacar lo solicitado");
                    btnGuardar.Enabled = true;
                }
            }

            return false;
        }

        private bool Validacion()
        {

            if(txtNumS.Text == string.Empty)
            {
                MessageBox.Show("Debe Agregar el número de salida");
                
            }else if(txtSku.Text == string.Empty)
            {
                MessageBox.Show("Debe agregar el Sku del producto");

            }else if(txtCantSal.Text == string.Empty){

                MessageBox.Show("Debe ingresar la cantidad de salida");
            }
            else
            {
                return true;
            }

            return false;   
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Insertar();
        }
    }
}

using Control;
using Practica;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Vista;

namespace Vista
{
    public partial class Form1 : Form
    {
        private Controlador control;
        public Form1()
        {
            InitializeComponent();
            control = new Controlador();
            //textBoxFiltrar_TextChanged();
        }        

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            int edad = DateTime.Today.Year - dateTimePickerFechNac.Value.Year;
            if (DateTime.Today.Month < dateTimePickerFechNac.Value.Month)
                edad--;
            else if (DateTime.Today.Month == dateTimePickerFechNac.Value.Month)
                if (DateTime.Today.Day < dateTimePickerFechNac.Value.Day)
                    edad--;
            textEdad.Text = edad.ToString();
        }

        private void buttonGuardar_Click(object sender, EventArgs e)
        {
            String resultado = null;

            if (textBoxCedula.Text != "")
            {
                control.setPersona(new Persona());
                control.getPersona().setCedula(int.Parse(textBoxCedula.Text));
                control.getPersona().setCedula(int.Parse(textBoxCedula.Text));
                control.getPersona().setNombre(textBoxNombre.Text);
                control.getPersona().setFechaNac(dateTimePickerFechNac.Value);
                Char sexo;
                if (radioButtonHombre.Checked == true)
                    sexo = 'H';
                else
                    sexo = 'M';
                control.getPersona().setSexo(sexo);
                control.getPersona().setProfesion(comboBoxProfesion.Text);
                control.getPersona().setTelefono(int.Parse(textBoxTelefono.Text));

                byte[] foto = imageToByteArray(pictureBoxFoto.Image);
                control.getPersona().setFoto(foto);

                if (control.getConexionDB().existePersona(int.Parse(textBoxCedula.Text)) == true)
                {
                    DialogResult dialogResult = MessageBox.Show("Ya existe esta persona guardada, ¿Desea modificar los datos almacenados anteriormente?",
                        "Persona ya registrada", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dialogResult == DialogResult.Yes)
                        resultado = control.getConexionDB().actualizarPersona(control.getPersona());
                    else
                        return;    
                }
                else
                    resultado = control.getConexionDB().insertarPersona(control.getPersona());
                if (resultado == null)
                {
                    MessageBox.Show("Almacenado correctamente");
                    LimpiarCampos();
                }
                else
                {
                    MessageBox.Show(resultado, "Error al guardar persona", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                if (dataGridView1.RowCount > 0)
                    textBoxFiltrar_Buscar(sender, e);
            }                
        }

        public byte[] imageToByteArray(System.Drawing.Image imageIn)
        {
            using (var ms = new MemoryStream())
            {
                imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
                return ms.ToArray();
            }
        }

        private void buttonFoto_Click(object sender, EventArgs e)
        {
            // Se crea el OpenFileDialog
            OpenFileDialog dialog = new OpenFileDialog();
            // Se muestra al usuario esperando una acción
            DialogResult result = dialog.ShowDialog();

            // Si seleccionó un archivo (asumiendo que es una imagen lo que seleccionó)
            // la mostramos en el PictureBox de la inferfaz
            if (result == DialogResult.OK)
            {
                pictureBoxFoto.Image = Image.FromFile(dialog.FileName);
                Image flipImage = pictureBoxFoto.Image;
                flipImage.RotateFlip(RotateFlipType.Rotate270FlipXY);
                pictureBoxFoto.Image = flipImage;
            }
        }

        private void textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void LimpiarCampos(object sender=null, EventArgs e=null)
        {
            textBoxCedula.Text = "";
            textBoxNombre.Text = "";
            textBoxTelefono.Text = "";            
            dateTimePickerFechNac.Value = DateTime.Today;
            textEdad.Text = "";
            radioButtonHombre.Checked = false;
            radioButtonMujer.Checked = false;
            comboBoxProfesion.Text = "";
            pictureBoxFoto.Image = null;
            //while (dataGridView1.RowCount > 0)
            //    dataGridView1.Rows.Remove(dataGridView1.CurrentRow);
        }

        private void BuscarPersona(object sender, EventArgs e)
        {
            if (textBoxCedula.Text != "")
            {
                control.setPersona(new Persona());
                control.getPersona().setCedula(int.Parse(textBoxCedula.Text));
                if (control.getConexionDB().buscarPersona(control.getPersona()) == true)
                {
                    textBoxNombre.Text = control.getPersona().getNombre();
                    textBoxTelefono.Text = control.getPersona().getTelefono().ToString();
                    dateTimePickerFechNac.Value = control.getPersona().getFechaNac();
                    if (control.getPersona().getSexo() == 'H')
                        radioButtonHombre.Checked = true;
                    else
                        radioButtonMujer.Checked = true;
                    comboBoxProfesion.Text = control.getPersona().getProfesion();
                    MemoryStream imagen = new MemoryStream(control.getPersona().getFoto());
                    pictureBoxFoto.Image = Image.FromStream(imagen);
                }
                else
                    MessageBox.Show("No se encontro persona");
            }
        }

        private void textBoxFiltrar_Buscar(object sender, EventArgs e)
        {
            String filtrar = textBoxFiltrar.Text;
            DataTable tabla = control.getConexionDB().FiltrarPersonas(filtrar);

            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            dataGridView1.DataSource = tabla;
            dataGridView1.Columns[0].DataPropertyName = tabla.Columns[0].ColumnName;
            dataGridView1.Columns[1].DataPropertyName = tabla.Columns[1].ColumnName;
            dataGridView1.Columns[2].DataPropertyName = tabla.Columns[2].ColumnName;
            dataGridView1.Columns[3].DataPropertyName = tabla.Columns[3].ColumnName;
            dataGridView1.Columns[4].DataPropertyName = tabla.Columns[5].ColumnName;
            dataGridView1.Columns[5].DataPropertyName = tabla.Columns[4].ColumnName;           
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            textBoxCedula.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            BuscarPersona(sender, null);
        }

        private void pictureGirar_Click(object sender, EventArgs e)
        {
            Image flipImage = pictureBoxFoto.Image;
            flipImage.RotateFlip(RotateFlipType.Rotate270FlipXY);
            pictureBoxFoto.Image = flipImage;
        }

        private void buttonEliminar_Click(object sender, EventArgs e)
        {
            if (textBoxCedula.Text != "")
            {
                if (control.getConexionDB().existePersona(int.Parse(textBoxCedula.Text)) == true)
                {
                    DialogResult dialogresult = MessageBox.Show("¿Esta seguro de eliminar todos los datos de esa persona?", "Eliminar Persona", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                    if (dialogresult == DialogResult.Yes)
                    {
                        if (control.getConexionDB().eliminarPersona(int.Parse(textBoxCedula.Text)) == true)
                            MessageBox.Show("Persona eliminada de forma correcta");
                        else
                            MessageBox.Show("Error al eliminar");
                        if (dataGridView1.RowCount > 0)
                            textBoxFiltrar_Buscar(sender, e);
                    }
                }
            }
            
        }

        private void buttonReporte_Click(object sender, EventArgs e)
        {
            Form2 frm = new Form2();
            frm.Show();
        }
    }
}

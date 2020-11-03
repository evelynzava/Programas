using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;
using System.Windows.Forms;


namespace ClasesVirtualesProgramacion
{
    class admConexion
    {
        //Dar acceso publico al objeto para administrar la conexion ala base de datos
        public MySqlConnection oConexion;
        /*Funcion para abrir una conexion, si el estado de la misma es abierto,
         * entonces esta sea cerrado para abrir una nueva conexion */
         public bool AbrirConexion()
        {
            bool conectado = false;
            string servidor = "localhost", puerto = "3306";
            string usuario = "root", contrasena = "root", BD = "clasesvirtuales";
            string cadenaConexion = string.Format("datasource={0}; port={1}; username={2}; password={3};database={4}", servidor,puerto,usuario,contrasena,BD);

            try
            {
                if (oConexion != null && oConexion.State == ConnectionState.Open)
                    oConexion.Close();

                oConexion = new MySqlConnection(cadenaConexion);
                oConexion.Open();
                conectado = true;

            }
            catch(MySqlException Exception)
            {
                MessageBox.Show(Exception.Message, "Error en conexión", MessageBoxButtons.OK, MessageBoxIcon.Information);
                conectado = false;
            }
            return conectado;
        }

        /*Funcion que recive una SQL para hacer una peticion select a la BD y poblar una tabla dentro de un DataSet */
        public bool selectData(string SQL, DataTable Tabla)
        {
            bool ejecucionCorrecta = false;
            if(this.AbrirConexion()==true)
            {
                try
                {
                    MySqlDataAdapter da = new MySqlDataAdapter(SQL, oConexion);
                    da.Fill(Tabla);
                    ejecucionCorrecta = true;
                    oConexion.Close();
                }
                catch(MySqlException Exception)
                {
                    MessageBox.Show(Exception.Message, "Error en SQL", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ejecucionCorrecta = false;
                }
            }
            return ejecucionCorrecta;
        }

        /*Funcion para ejecutar instrucciones SQL de accion insert, update, delete */
        public bool AccionSQL(string SQL)
        {
            bool ejecucionCorrecta = false;
            if(this.AbrirConexion()==true)
            {
                try
                {
                    MySqlCommand cmd = new MySqlCommand(SQL, oConexion);
                    cmd.ExecuteNonQuery();
                    ejecucionCorrecta = true;
                    oConexion.Close();
                }
                catch(MySqlException Exception)
                {
                    MessageBox.Show(Exception.Message, "Error en SQL de acción ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ejecucionCorrecta = false;
                }
            }
            return ejecucionCorrecta;
        }
    }
}
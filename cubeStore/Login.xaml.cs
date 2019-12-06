﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Common;
using BRL;
using System.Data;

namespace cubeStore
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
		UsuarioBRL brl;
        public Login()
        {
            InitializeComponent();
        }

		private void BtnSalir_Click(object sender, RoutedEventArgs e)
		{
			if (MessageBox.Show("Esta Seguro de Salir?", "Salir", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
			{
				this.Close();
			}
			
		}

		private void BtnIngresar_Click(object sender, RoutedEventArgs e)
		{
			txtUusario.Text = txtUusario.Text.Trim();
			if (txtUusario.Text!=""&&txtContrasenia.Password!="")
			{
				try
				{
					brl = new UsuarioBRL();
					DataTable dt = brl.Login(txtUusario.Text, txtContrasenia.Password);
					if (dt.Rows.Count>0)
					{
						if (byte.Parse(dt.Rows[0][3].ToString()) == 1)
						{
							CambiarContrasenia cambia = new CambiarContrasenia();
							this.Close();
							cambia.Show();
						}
						else { 
							//Iniciamos variable de sesion
						Sesion.idSesion= int.Parse(dt.Rows[0][0].ToString());
						Sesion.usuarioSesion= dt.Rows[0][1].ToString();
						Sesion.rolSesion= dt.Rows[0][2].ToString();

						//Iniciamos variables de configuracion
						ConfigBRL configBRL = new ConfigBRL();
						DataTable dtConfig = configBRL.Select();
						Config.configPathImagen = dtConfig.Rows[0][0].ToString();
						if (dt.Rows[0][2].ToString() == "Administrador")
						{
							MainWindow menu = new MainWindow();
							this.Visibility = Visibility.Hidden;
							menu.Show();
						}
						else if (dt.Rows[0][2].ToString() == "Editor")
						{
							MenuEditor menuedit = new MenuEditor();
							this.Visibility = Visibility.Hidden;
							menuedit.Show();
						}
						else if (dt.Rows[0][2].ToString() == "Vendedor")
						{
							MenuVendedor menVend = new MenuVendedor();
							this.Visibility = Visibility.Hidden;
							menVend.Show();
						}
						}


					}
					else
					{
						MessageBox.Show("Usuario o contrasenia Incorrectos");
						txtUusario.Text = "";
						txtContrasenia.Password = "";
					}
				}
				catch (Exception ex)
				{

					MessageBox.Show("Error"+ex.Message);
				}
			}
			else
			{
				MessageBox.Show("Tiene que llenar los campos para ingresar");
			}
			
		}

		private void BtnRestablecerContraseña_Click(object sender, RoutedEventArgs e)
		{
			RestablecerContraseña restablecerContraseña = new RestablecerContraseña();
			this.Close();
			restablecerContraseña.Show();
		}
	}
}

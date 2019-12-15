﻿using BRL;
using Common;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace cubeStore
{
	/// <summary>
	/// Interaction logic for Categorias.xaml
	/// </summary>
	public partial class Categorias : Window
	{
		DataTable dtElim;
		byte operacion = 0;
		CategoriaBRL brl;
		Categoria categoria;
		public Categorias()
		{
			InitializeComponent();
		}
		private void LoadDataGrid()
		{
			try
			{
				brl = new CategoriaBRL();
				dgdDatos.ItemsSource = brl.Select().DefaultView;
				dgdDatos.ItemsSource = brl.SelectBusquedaCategorias(txtBuscarCategorias.Text).DefaultView;
				dgdDatos.Columns[0].Visibility = Visibility.Hidden;
			}
			catch (Exception ex)
			{

				MessageBox.Show(ex.Message);
			}

		}
		private void Habilitar(byte operacion)
		{
			txtnombreCategoria.IsEnabled = true;

			btnGuardarAccion.IsEnabled = true;
			btnCancelar.IsEnabled = true;							
			btnAgregarUsuario.IsEnabled = false;
			btnModificar.IsEnabled = false;
			btnEliminar.IsEnabled = false;
			this.operacion = operacion;
		}

		private void DesHabilitar()
		{
			txtnombreCategoria.IsEnabled = false;
			btnGuardarAccion.IsEnabled = false;
			btnCancelar.IsEnabled = false;				
			btnAgregarUsuario.IsEnabled = true;
			btnModificar.IsEnabled = true;
			btnEliminar.IsEnabled = true;
		}
		void LimpiarCampos()
		{
			txtnombreCategoria.Text = "";			
		}

		private void BtnCerrarArticulos_Click(object sender, RoutedEventArgs e)
		{
			dgdDatos.IsEnabled = true;
			this.Close();

		}
		/// <summary>
		/// Evento click que permite modificar una Categoria
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnModificar_Click(object sender, RoutedEventArgs e)
		{
			Habilitar(2);
			dgdDatos.IsEnabled = true;
		}
		/// <summary>
		/// Evento click que permite agregar una nueva Categoria
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnAgregarUsuario_Click(object sender, RoutedEventArgs e)
		{			
			Habilitar(1);
			LimpiarCampos();
			dgdDatos.IsEnabled = false;
		}
		/// <summary>
		/// Evento Click para eliminar una categoria
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnEliminar_Click(object sender, RoutedEventArgs e)
		{
			
			if (dtElim!=null)
			{			
				brl = new CategoriaBRL();
				dtElim = brl.VerificarCategoriaEliminar(categoria.IdCategoria);			
				if (dtElim.Rows.Count == 0 &&dgdDatos.Items.Count<1)
				{
					if (categoria != null && txtnombreCategoria.Text != "")
					{
						if (MessageBox.Show("Esta Seguro de Eliminar la categoria?", "Eliminar", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
						{
							//Eliminacion Logica
							try
							{
								brl = new CategoriaBRL(categoria);
								brl.Delete();
								MessageBox.Show("Eliminado Exitosamente");
								LoadDataGrid();
								LimpiarCampos();
							}
							catch (Exception)
							{
								MessageBox.Show("Ocurrio un error al eliminar la categoria intente nuevamente si el error persiste comuniquese con el administrador de sistemas");
							}
						}
					}
				else
				{
					MessageBox.Show("Tiene que seleccionar un registro de la lista para eliminarlo");
				}
			}
			else
			{
				MessageBox.Show("No puede eliminar esta Categoria por que tiene Articulos que estan asociados a ella");
			}
			}
			else
			{
				MessageBox.Show("Seleccione un registro de la lista para poder eliminarlo");
			}
		}

		private void BtnGuardarAccion_Click(object sender, RoutedEventArgs e)
		{
			switch (operacion)
			{
				case 1:
					//Insertar
					if (txtnombreCategoria.Text == "")
					{
						MessageBox.Show("Debe Llenar los campos para poder agregar un registro");
					}
					else
					{
						txtnombreCategoria.Text = txtnombreCategoria.Text.Trim();
						brl = new CategoriaBRL();
						DataTable dt = brl.VerificarCategoria(txtnombreCategoria.Text);
						if (dt.Rows.Count == 0)
						{
							if (Validate.OnlyLettersAndSpaces(txtnombreCategoria.Text))
							{
								try
								{
									dgdDatos.IsEnabled = false;
									//Insertar
									DateTime fechahora = DateTime.Now;
									categoria = new Categoria(txtnombreCategoria.Text, fechahora);
									brl = new CategoriaBRL(categoria);
									brl.Insert();
									MessageBox.Show("Registro Exitoso");
									LoadDataGrid();
									DesHabilitar();
									LimpiarCampos();
									dgdDatos.IsEnabled = true;
								}
								catch (Exception)
								{

									MessageBox.Show("Existe un problema al insertar el registro, comuniquese con el administrador de sistemas");
								}

							}
							else
							{
								MessageBox.Show("Ingrese Correctamente el nombre");
								LimpiarCampos();
							}
						}
						else
						{
							MessageBox.Show("La categoria ya existe");
						}
					}
					break;
				case 2:
					//Modificar
					if (txtnombreCategoria.Text==""&&categoria==null)
					{
						MessageBox.Show("Tiene que seleccionar un registro de la lista para modificarlo");
					}
					else
					{
						txtnombreCategoria.Text = txtnombreCategoria.Text.Trim();
						brl = new CategoriaBRL();
						DataTable dt = brl.VerificarCategoria(txtnombreCategoria.Text);
						if (dt.Rows.Count == 0)
						{

							if (Validate.OnlyLettersAndSpaces(txtnombreCategoria.Text))
							{
								try
								{
									dgdDatos.IsEnabled = true;
									//Modificar
									//categoria = new Categoria(txtnombreCategoria.Text);
									categoria.NombreCategoria = txtnombreCategoria.Text;

									brl = new CategoriaBRL(categoria);
									brl.Update();
									MessageBox.Show("Registro Modificado Exitosamente");
									LoadDataGrid();
									DesHabilitar();
									LimpiarCampos();
								}
								catch (Exception ex)
								{

									MessageBox.Show("Ocurrio un error comuniquese con el administrador de sistemas");
								}

							}
							else
							{
								MessageBox.Show("Ingrese Correctamente el nombre");
							}
						}
						else
						{
							MessageBox.Show("La categoria ya existe");
						}
					}
					break;
			}
		}

		private void BtnCancelar_Click(object sender, RoutedEventArgs e)
		{
			DesHabilitar();
			LimpiarCampos();
			dgdDatos.IsEnabled = true;
		}
		/// <summary>
		/// Evento Loaded para cargar al datagrid los registros de Articulos
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			LoadDataGrid();
			dgdDatos.IsEnabled = true;
		}
		/// <summary>
		/// Evento SelectionChanged para poder seleccionar una Categoria de un DataGrid
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DgdDatos_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{

			if (dgdDatos.Items.Count > 0 && dgdDatos.SelectedItem != null)
			{
				//Realizamos Get
				try
				{
					DataRowView dataRow = (DataRowView)dgdDatos.SelectedItem;
					byte id = byte.Parse(dataRow.Row.ItemArray[0].ToString());
					brl = new CategoriaBRL();
					categoria = brl.Get(id);

					//Cargar Datos
					txtnombreCategoria.Text = categoria.NombreCategoria;
				}
				catch (Exception ex)
				{
					MessageBox.Show("Ocurrio un error comuniquese con el administrador de sistemas");
				}
			}
		}
		/// <summary>
		/// Evento TextChanged para realizar busquedas de Categorias
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TxtBuscarCategorias_TextChanged(object sender, TextChangedEventArgs e)
		{
			if (txtBuscarCategorias.Text == "")
			{
				LoadDataGrid();
			}
			else
			{
				LoadDataGrid();
			}
		}
	}
}

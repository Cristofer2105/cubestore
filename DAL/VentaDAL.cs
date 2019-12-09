﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Common;

namespace DAL
{
	public class VentaDAL : AbstractDAL
	{
		#region Atributos Propiedades y Constructores de clase
		private Venta vtn;

		public Venta Vtn
		{
			get { return vtn; }
			set { vtn = value; }
		}
		private List<VentaItem> vti;

		public List<VentaItem> Vti
		{
			get { return vti; }
			set { vti = value; }
		}
		private Garantia grt;

		public Garantia Grt
		{
			get { return grt; }
			set { grt = value; }
		}
		private Cliente cli;

		public Cliente Cli
		{
			get { return cli; }
			set { cli = value; }
		}
		private Item itm;

		public Item Art
		{
			get { return itm; }
			set { itm = value; }
		}
		public VentaDAL(Venta vtn,List<VentaItem> vti,Garantia grt)
		{
			this.vtn=vtn;
			this.vti = vti;
			this.grt = grt;
		}
		public VentaDAL()
		{

		}
		public VentaDAL(Venta vtn)
		{
			this.vtn = vtn;
		}
		
		#endregion
		#region metodos de la clase
		public override void Delete()
		{
			throw new NotImplementedException();
		}

		public void InsertVentas()
		{

			SqlConnection connection = Methods.GetConnection();
			connection.Open();

			SqlCommand command = connection.CreateCommand();
			//inicio transaccion
			SqlTransaction transaction;

			transaction = connection.BeginTransaction("Venta");
			command.Connection = connection;
			command.Transaction = transaction;
			try
			{
				int id = Methods.GetCurrentValueIDTable("Venta");
				//query 1 Venta
				command.CommandText = "INSERT INTO Venta(idCliente,total,idEmpleado,fechaRegistro) VALUES(@idCliente,@total,@idEmpleado,@fechaRegistro)";
				command.Parameters.AddWithValue("@idCliente", vtn.IdCliente);
				command.Parameters.AddWithValue("@total", vtn.Total);
				command.Parameters.AddWithValue("@idEmpleado", vtn.IdEmpleado);
				command.Parameters.AddWithValue("@fechaRegistro", vtn.FechaRegistroVenta);
				command.ExecuteNonQuery();


				//query Venta Item
				command.CommandText = "INSERT INTO VentaItem(idVenta,idItem,precioUnitario) VALUES(@idVenta,@idItem,@precioUnitario)";
				foreach (var producto in this.vti)
				{
					command.Parameters.AddWithValue("@idVenta", id);
					command.Parameters.AddWithValue("@idItem", producto.IdItem);
					command.Parameters.AddWithValue("@precioUnitario", producto.PrecioUnitario);
					command.ExecuteNonQuery();
					command.Parameters.Clear();

				}

				//query Garantia
				command.CommandText = "INSERT INTO Garantia(idGarantia,fechaInicioGarantia,fechafinGarantia,fechaRegistroGarantia) VALUES(@idGarantia,@fechaInicioGarantia,@fechafinGarantia,@fechaRegistroGarantia)";
				command.Parameters.AddWithValue("@idGarantia", id);
				command.Parameters.AddWithValue("@fechaInicioGarantia", grt.FechaInicio);
				command.Parameters.AddWithValue("@fechafinGarantia", grt.FechaFin);
				command.Parameters.AddWithValue("@fechaRegistroGarantia", grt.FechaRegistro);
				command.ExecuteNonQuery();

				//qury estado
				command.CommandText = "UPDATE Item SET estadoItem=3 WHERE estadoItem=2";
				command.ExecuteNonQuery();

				// Attempt to commit the transaction.
				transaction.Commit();
			}
			catch (Exception ex)
			{
				transaction.Rollback();
				string query = "UPDATE Item SET estadoItem=1 WHERE estadoItem=2";
				SqlCommand cmd = Methods.CreateBasicCommand(query);
				Methods.ExecuteBasicCommand(cmd);
				//Escribir Log
				throw ex;

			}
		}

		public override DataTable Select()
		{
			throw new NotImplementedException();
		}

		public override void Update()
		{
			throw new NotImplementedException();
		}

		public override void Insert()
		{
			throw new NotImplementedException();
		}
		#endregion
	}
}
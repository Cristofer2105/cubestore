﻿using DAL;
using System.Data;
using Common;

namespace BRL
{
	public class UsuarioBRL : AbstractBRL
	{
		
		private Usuario user;

		public Usuario User
		{
			get { return user; }
			set { user = value; }
		}
		private UsuarioDal dal;

		public UsuarioDal Dal
		{
			get { return dal; }
			set { dal = value; }
		}
		public UsuarioBRL()
		{

		}
		public UsuarioBRL(Usuario user)
		{
			this.user = user;
			dal = new UsuarioDal(user);
		}
		

		public override void Delete()
		{
			dal.Delete();
		}

		public override void Insert()
		{
			dal.Insert();
		}

		public override DataTable Select()
		{
			dal = new UsuarioDal();
			return dal.Select();
		}

		public override void Update()
		{
			dal.Update();
		}
		public void UpdateDatosPerfil()
		{
			dal.UpdateDatosPerfil();
		}
		public void UpdateContrasenia()
		{
			dal.UpdateContrasenia();
		}
		public void UpdateContraseniaRestablecida()
		{
			dal.UpdateContraseniaRestablecida();
		}
		
		public DataTable Login(string usuario, string contrasenia)
		{
			dal = new UsuarioDal();
			return dal.Login(usuario, contrasenia);
		}
		public DataTable VerificarUser(string usuario, string correo)
		{
			dal = new UsuarioDal();
			return dal.Login(usuario, correo);
		}
		
		public DataTable RestablecerContrasenia(string usuario)
		{
			dal = new UsuarioDal();
			return dal.RestablecerContrasenia(usuario);
		}
		public Usuario Get(int idUsuario)
		{
			dal = new UsuarioDal();
			return dal.Get(idUsuario);
		}
		public DataTable SelectBusquedaUsarios(string texto)
		{
			dal = new UsuarioDal();
			return dal.SelectBusquedaUsarios(texto);
		}
		
	}
}

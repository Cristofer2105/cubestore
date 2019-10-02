﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Common;
using DAL;

namespace BRL
{
	public class CategoriaBRL : AbstractBRL
	{
		#region Atributos propiedades y constructores
		private Categoria cat;

		public Categoria Cat
		{
			get { return cat; }
			set { cat = value; }
		}

		private CategoriaDAL dal;

		public CategoriaDAL Dal
		{
			get { return dal; }
			set { dal = value; }
		}
		public CategoriaBRL()
		{
		
		}
		public CategoriaBRL(Categoria cat)
		{
			this.cat = cat;
			dal = new CategoriaDAL(cat);
		}
		#endregion
		#region Metodos de la Clase

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
			dal = new CategoriaDAL();
			return dal.Select();
		}

		public override void Update()
		{
			dal.Update();
		}
		public Categoria Get(byte idCategoria)
		{
			dal = new CategoriaDAL();
			return dal.Get(idCategoria);
		}
		#endregion
	}
}
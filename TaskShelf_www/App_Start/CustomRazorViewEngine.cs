using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TaskShelf_www.App_Start
{
	public class CustomRazorViewEngine : RazorViewEngine
	{
		public CustomRazorViewEngine()
			: base()
		{

			ViewLocationFormats = new[] {
				"~/Parts/%1/Views/{0}.cshtml",	            
				"~/Parts/%1/Views/Shared/{0}.cshtml",	 
				"~/Views/{0}.cshtml",	            
				"~/Views/Shared/{0}.cshtml"	    
			};

			MasterLocationFormats = new[] {
				"~/Views/{0}.cshtml",	            
				"~/Views/Shared/{0}.cshtml"	            
			};

			PartialViewLocationFormats = new[] {
				"~/Parts/%1/Views/{0}.cshtml",	            
				"~/Parts/%1/Views/Shared/{0}.cshtml",
				"~/Views/{0}.cshtml",	            
				"~/Views/Shared/{0}.cshtml"	 
			};
		}

		protected override IView CreatePartialView(ControllerContext controllerContext, string partialPath)
		{
			var nameSpace = controllerContext.Controller.GetType().Namespace;
			return base.CreatePartialView(controllerContext, partialPath.Replace("%1", GetLastNameSpacePart(nameSpace)));
		}

		protected override IView CreateView(ControllerContext controllerContext, string viewPath, string masterPath)
		{
			var nameSpace = controllerContext.Controller.GetType().Namespace;
			return base.CreateView(controllerContext, viewPath.Replace("%1", GetLastNameSpacePart(nameSpace)), masterPath.Replace("%1", GetLastNameSpacePart(nameSpace)));
		}

		protected override bool FileExists(ControllerContext controllerContext, string virtualPath)
		{
			var nameSpace = controllerContext.Controller.GetType().Namespace;
			var exists = base.FileExists(controllerContext, virtualPath.Replace("%1", GetLastNameSpacePart(nameSpace)));
			return exists;
		}

		private string GetLastNameSpacePart(string nameSpace)
		{
			var nameSpaceParts = nameSpace.Split(".".ToCharArray());
			return nameSpaceParts.Last();
		}
	}
}
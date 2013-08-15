using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


[assembly: WebActivatorEx.PostApplicationStartMethod(typeof(EnlightenAss.App_Start.EntityFrameworkMigrations), "PostStart")]
namespace EnlightenAss.App_Start
{
	using System.Data.Entity.Migrations;
     	
    public static class EntityFrameworkMigrations 
    {
        public static void PostStart() 
        {           
     	     	DbMigrator dbMigrator = new DbMigrator(new AssClassLibrary.Migrations.Configuration());   	     	  
             	dbMigrator.Update();                                     	  
        }        
    }
}

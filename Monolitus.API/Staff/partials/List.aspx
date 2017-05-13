<%@ Page Title="" Language="C#" AutoEventWireup="true" %>

<%@ Import Namespace="System.Reflection" %>
<%@ Import Namespace="Monolitus.API" %>
<%@ Import Namespace="Monolitus.API.Entity" %>
<%@ Import Namespace="System.IO" %>
<%
    if (File.Exists(Server.MapPath("/Staff/Partials/List" + Request["entity"] + ".aspx")))
    {
        Response.Redirect("/Staff/Partials/List" + Request["entity"] + ".aspx", true);
        return;
    }

    var type = typeof(BaseEntity).Assembly.GetType("Monolitus.API.Entity.ListView" + Request["entity"]);
    if(type==null)
        type = typeof(BaseEntity).Assembly.GetType("Monolitus.API.Entity." + Request["entity"]);
    if (type == null)
    {
        Response.Write("Entity not found: " + "Monolitus.API.Entity." + Request["entity"]);
        return;
    }

    
%>
<list-header title="<%=type.Name.Replace("ListView","") %>s"></list-header>

<page-size></page-size>
<pagination></pagination>

<table class="table table-striped table-bordered table-hover">
    <thead>
        <tr>
            <th>#</th>
<% 
                foreach (var pi in type.GetProperties())
                {
                    if (pi.Name == "InsertDate")
                        Response.Write("           <th column-header=\"Added\" field=\"InsertDate\"></th>\r\n");
                    else if (pi.Name == "Name")
                        Response.Write("           <th column-header=\"" + pi.Name + "\" field=\"" + pi.Name + "\"></th>\r\n");
                    else if (pi.Name.EndsWith("Id") || pi.Name=="IsDeleted" || pi.Name=="Item")
                        Response.Write("");
                    else if (pi.Name.EndsWith("Name"))
                    {
                        var entityName = pi.Name.Substring(0, pi.Name.Length - 4);
                        Response.Write("           <th column-header=\"" + entityName + "\" field=\"" + pi.Name + "\">\r\n");
                    }
                    else
                        Response.Write("           <th column-header=\"" + pi.Name + "\" field=\"" + pi.Name + "\"></th>\r\n");
                }
%>
            <th></th>
        </tr>
    </thead>
    <tbody>
        <tr ng-repeat="entity in list" ng-class="{deleted:entity.IsDeleted}">
            <td indexer></td>
<% 
                foreach (var pi in type.GetProperties())
                {
                    if (pi.Name == "InsertDate")
                        Response.Write("            <td>{{entity.InsertDate | date:'dd-MM-yyyy'}}</td>\r\n");
                    else if (pi.Name == "Name")
                        Response.Write("            <td link-to-view>{{entity.Name}}</td>\r\n");
                    else if (pi.Name.EndsWith("Id") || pi.Name=="IsDeleted" || pi.Name=="Item")
                        Response.Write("");
                    else if (pi.Name.EndsWith("Name"))
                    {
                        var entityName = pi.Name.Substring(0, pi.Name.Length - 4);
                        Response.Write("            <td link-to-parent=\"" + entityName + "\">{{entity." + pi.Name + "}}</td>\r\n");
                    }
                    else if(pi.Name == "Html")
                        Response.Write("            <td ng-bind-html=\"entity." + pi.Name + "\"></td>\r\n");
                    else
                        Response.Write("            <td>{{entity." + pi.Name + "}}</td>\r\n");
                }
%>
            <td operations></td>
        </tr>
    </tbody>
</table>

<list-footer no-add-new="1"></list-footer>
<%@ Page Language="C#" AutoEventWireup="true" %>

<%@ Import Namespace="Monolitus.API" %>
<%@ Import Namespace="Monolitus.DTO" %>
<%@ Import Namespace="Monolitus.DTO.Enums" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Monolitus Admin</title>
    <link href="/Assets/css/bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="/Assets/css/font-awesome.min.css" />

    <!--link rel="stylesheet" href="/Assets/css/jquery-ui-1.10.3.full.min.css" /-->
    <link rel="stylesheet" href="/Assets/css/datepicker.css" />
    <!--link rel="stylesheet" href="/Assets/css/ui.jqgrid.css" /-->
    <link rel="stylesheet" href="/Assets/css/chosen.css" />
    <link rel="stylesheet" href="/Assets/css/bootstrap-timepicker.css" />
    <link rel="stylesheet" href="/Assets/css/daterangepicker.css" />
    <!--link rel="stylesheet" href="/Assets/css/colorpicker.css" /-->

    <!--[if IE 7]>
		  <link rel="stylesheet" href="/Assets/css/font-awesome-ie7.min.css" />
		<![endif]-->

    <!-- page specific plugin styles -->

    <!-- fonts -->

    <link rel="stylesheet" href="/Assets/css/ace-fonts.css" />

    <!-- ace styles -->

    <link rel="stylesheet" href="/Assets/css/uncompressed/ace.css" />
    <link rel="stylesheet" href="/Assets/css/uncompressed/ace-rtl.css" />
    <link rel="stylesheet" href="/Assets/css/uncompressed/ace-skins.css" />

    <!--[if lte IE 8]>
		  <link rel="stylesheet" href="/Assets/css/ace-ie.min.css" />
		<![endif]-->

    <!-- inline styles related to this page -->

    <!-- ace settings handler -->

    <script src="/Assets/js/ace-extra.min.js"></script>
    <!-- HTML5 shim and Respond.js IE8 support of HTML5 elements and media queries -->

    <!--[if lt IE 9]>
		<script src="/Assets/js/html5shiv.js"></script>
		<script src="/Assets/js/respond.min.js"></script>
		<![endif]-->

    <script type="text/javascript">
        window.jQuery || document.write("<script src='/Assets/js/jquery-2.0.3.min.js'>" + "<" + "/script>");
    </script>

    <!--[if IE]>
        <script type="text/javascript">
         window.jQuery || document.write("<script src='/Assets/js/jquery-1.10.2.min.js'>"+"<"+"/script>");
        </script>
        <![endif]-->

    <script type="text/javascript">
        if ("ontouchend" in document) document.write("<script src='assets/js/jquery.mobile.custom.min.js'>" + "<" + "/script>");
    </script>
    <script src="/Assets/js/bootstrap.min.js"></script>
    <!-- page specific plugin scripts -->

    <!--[if lte IE 8]>
		  <script src="/Assets/js/excanvas.min.js"></script>
		<![endif]-->

    <link href="/Assets/default.css" rel="stylesheet" />

</head>
<body class="login-layout">
    <div class="main-container">
        <div class="main-content">
            <div class="row">
                <div class="col-sm-10 col-sm-offset-1">
                    <div class="login-container">
                        <div class="center">
                            <%-- <img src="{{entity.ThumbnailPicture}}" />--%>
                            <h1 style="color: white;  background: #5090C1;  margin: 20px 80px;  padding: 10px;">
                                MONOLITUS
                            </h1>
                            <br />
                            <br />
                        </div>

                        <div class="space-6"></div>

                        <div class="position-relative">
                            <div id="login-box" class="login-box visible widget-box no-border">
                                <div class="widget-body">
                                    <div class="widget-main">
                                        <h4 class="header blue lighter bigger">
                                            <i class="icon-coffee green"></i>
                                            Please log in
                                        </h4>

                                        <div class="space-6"></div>

                                        <div id="Content" class="container" class="Region navbar navbar-inverse navbar-fixed-top">
                                            <div id="LoginForm2_50" class="Module LoginForm2 row" mid="LoginForm2_50">
                                                <form accept-charset="UTF-8" role="form" id="fLogin" method="post" action="/Staff/Handlers/DoLogin.ashx">
                                                    <input type="hidden" name="RedirectUrl" value="<%=Request["RedirectUrl"] %>" />
                                                    <fieldset>
                                                        <label class="block clearfix">
                                                            <span class="block input-icon input-icon-right">
                                                                <input type="text" id="username" name="Email" class="form-control" placeholder="E-mail" />
                                                                <i class="icon-user"></i>
                                                            </span>
                                                        </label>

                                                        <label class="block clearfix">
                                                            <span class="block input-icon input-icon-right">
                                                                <input type="password" id="pass" class="form-control" placeholder="Password" name="Passwd" />
                                                                <i class="icon-lock"></i>
                                                            </span>
                                                        </label>

                                                        <div class="space"></div>

                                                        <div class="clearfix">
                                                            <button type="submit" class="width-35 pull-right btn btn-sm btn-primary" id="login">
                                                                <i class="icon-key"></i>
                                                                Enter
                                                            </button>
                                                        </div>

                                                        <div class="space-4"></div>
                                                    </fieldset>
                                                </form>

                                            </div>
                                        </div>

                                    </div>
                                    <!-- /widget-main -->

                                    <div class="toolbar clearfix">
                                        <div>
                                            <h6></h6>
                                        </div>
                                    </div>
                                </div>
                                <!-- /widget-body -->
                            </div>
                            <!-- /login-box -->
                        </div>
                        <!-- /position-relative -->
                    </div>
                </div>
                <!-- /.col -->
            </div>
            <!-- /.row -->
        </div>
    </div>
    <!-- /.main-container -->

</body>
</html>

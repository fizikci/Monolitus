<%@ Page Language="C#" AutoEventWireup="true" %>

<%@ Import Namespace="Monolitus.API" %>
<%@ Import Namespace="Monolitus.DTO" %>
<%@ Import Namespace="Monolitus.DTO.Enums" %>
<!DOCTYPE html>
<html data-ng-app="dealerSafeApp">
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
    <link href="/Assets/css/colorbox.css" rel="stylesheet" />

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
    <link href="/Assets/js/jqvmap/jqvmap.css" media="screen" rel="stylesheet" type="text/css" />

    <link href="/Assets/default.css" rel="stylesheet" />
    <script src="/Assets/js/prototype.js"></script>
</head>
<body>
    <div class="Region navbar navbar-default" id="navbar">
        <script type="text/javascript">
            try {
                ace.settings.check('navbar', 'fixed');
            } catch (e) { }
        </script>


        <div id="navbar-container" class="navbar-container">

            <div class="navbar-header pull-left">
                <a href="/" class="navbar-brand">
                    MONOLITUS
                </a>
                <!-- /.brand -->
            </div>
            <!-- /.navbar-header -->

            <div class="navbar-header pull-right" role="navigation">
                <ul class="nav ace-nav">

                    <li class="light-blue">
                        <a data-toggle="dropdown" href="#" class="dropdown-toggle">
                            <img class="nav-user-photo" src="/Assets/avatars/user.jpg" />
                            <span class="user-info">
                                <small><%=Provider.TR("Welcome") %><br />
                                    <b><%=Provider.CurrentUser.Name%></b></small>

                            </span>

                            <i class="icon-caret-down"></i>
                        </a>

                        <ul class="user-menu pull-right dropdown-menu dropdown-yellow dropdown-caret dropdown-close">

                            <li class="divider"></li>

                            <li>
                                <a href="/Staff/Handlers/DoLogin.ashx?logout=1">
                                    <i class="icon-off"></i>
                                    <%=Provider.TR("Logout") %>
                                </a>
                            </li>
                        </ul>
                    </li>
                </ul>
                <!-- /.ace-nav -->
            </div>
            <!-- /.navbar-header -->


        </div>



    </div>

    <div class="main-container" id="main-container">
        <script type="text/javascript">
            try {
                ace.settings.check('main-container', 'fixed');
            } catch (e) { }
        </script>

        <div class="main-container-inner">
            <a class="menu-toggler" id="menu-toggler" href="#">
                <span class="menu-text"></span>
            </a>

            <div class="Region sidebar" id="sidebar">
                <script type="text/javascript">
                    try {
                        ace.settings.check('sidebar', 'fixed');
                    } catch (e) { }
                </script>


                <div id="sidebar-shortcuts" class="sidebar-shortcuts">

                    <div class="sidebar-shortcuts-large" id="sidebar-shortcuts-large">
                        <a class="btn btn-success" href="#/Edit/Company">
                            <i class="icon-signal"></i>
                        </a>

                        <button class="btn btn-info">
                            <i class="icon-pencil"></i>
                        </button>

                        <button class="btn btn-warning">
                            <i class="icon-group"></i>
                        </button>

                        <button class="btn btn-danger">
                            <i class="icon-cogs"></i>
                        </button>
                    </div>

                    <div class="sidebar-shortcuts-mini" id="sidebar-shortcuts-mini">
                        <span class="btn btn-success"></span>

                        <span class="btn btn-info"></span>

                        <span class="btn btn-warning"></span>

                        <span class="btn btn-danger"></span>
                    </div>

                </div>



                <ul class="nav nav-list">

                    <li>
                        <a href="#" class="dropdown-toggle">
                            <i class="icon-home"></i>
                            <span class="menu-text">Hoşgeldiniz </span>
                            <b class="arrow icon-angle-down"></b>
                        </a>

                        <ul class="submenu">
                            <li>
                                <a href="#/Dashboard">
                                    <i class="icon-dashboard"></i>
                                    <%=Provider.TR("Dashboard") %> 
			                    </a>
                            </li>
                        </ul>

                    </li>                   

                    <li>
                        
                        <a href="#" class="dropdown-toggle">
                            <i class="icon-certificate"></i>
                            <span class="menu-text">Yönetim </span>
                            <b class="arrow icon-angle-down"></b>
                        </a>

                        <ul class="submenu">
                            <li>
                                <a href="#/List/User">
                                    <i class="icon-flag"></i>
                                    Users
			                    </a>
                            </li>                           
                            <li>
                                <a href="#/List/Message">
                                    <i class="icon-comment"></i>
                                    Messages
			                    </a>
                            </li>                           
                        </ul>
                    </li>

                     <li>
                        
                        <a href="#" class="dropdown-toggle">
                            <i class="icon-cloud"></i>
                            <span class="menu-text">API</span>
                            <b class="arrow icon-angle-down"></b>
                        </a>

                        <ul class="submenu">
                            <li>
                                <a href="#/List/ApiSession">
                                    <i class="icon-cloud"></i>
                                    Sessions 
                                </a>
                            </li>

                            <li>
                                <a href="#/APIDemo">
                                    <i class="icon-cloud"></i>
                                    API Demo
			                    </a>
                            </li>

                            <li>
                                <a href="#/Logs">
                                    <i class="icon-hdd"></i>
                                    Logs
			                    </a>
                            </li>

                            <li>
                                <a href="#/LogReport">
                                    <i class="icon-th-list"></i>
                                    API Usage
			                    </a>
                            </li>

                        </ul>
                    </li>

                </ul>

                <div class="sidebar-collapse" id="sidebar-collapse">
                    <i class="icon-double-angle-left" data-icon1="icon-double-angle-left" data-icon2="icon-double-angle-right"></i>
                </div>

                <script type="text/javascript">
                    try { ace.settings.check('sidebar', 'collapsed') } catch (e) { }
                </script>

            </div>

            <div class="main-content">
                <div class="Region breadcrumbs" id="breadcrumbs">
                    <script type="text/javascript">
                        try { ace.settings.check('breadcrumbs', 'fixed') } catch (e) { }
                    </script>

                    <ul class="breadcrumb">
                        <li>
                            <i class="icon-home home-icon"></i>
                            <a href="/Default.aspx"><%=Provider.TR("Home") %></a>
                        </li>
                        <li>
                            <a href="#"><%=Provider.TR("Other Pages") %></a>
                        </li>
                        <li class="active"><%=Provider.TR("Blank Page") %></li>
                    </ul>

                    <div id="nav-search" class="nav-search">
                        <form class="form-search">
                            <span class="input-icon">
                                <input type="text" placeholder="<%=Provider.TR("Search ...") %>" class="nav-search-input" id="nav-search-input" autocomplete="off" onkeypress="if (event.keyCode == 13) {location.href = '/Staff/Default.aspx#/List/ViewMainEntities/Name LIKE ' + $('#nav-search-input').val();}" />
                                <i class="icon-search nav-search-icon"></i>
                            </span>
                        </form>
                    </div>

                </div>

                <div id="listForm" class="page-content">
                    <div class="row">
                        <div class="col-xs-12">
                            <div class="Region row" id="contentDiv" animated-view>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </div>
        <!-- /.main-container-inner -->

        <a href="#" id="btn-scroll-up" class="btn-scroll-up btn btn-sm btn-inverse">
            <i class="icon-double-angle-up icon-only bigger-110"></i>
        </a>
    </div>
    <!-- /.main-container -->

    <!-- basic scripts -->
    <script src="/Assets/js/jquery-ui-1.10.3.full.min.js"></script>
    <script src="/Assets/js/jquery-ui-1.10.3.custom.min.js"></script>
    <!--script src="/Assets/js/jquery.ui.touch-punch.min.js"></script-->
    <script src="/Assets/js/chosen.jquery.min.js"></script>
    <script src="/Assets/js/fuelux/fuelux.spinner.min.js"></script>
    <script src="/Assets/js/date-time/bootstrap-datepicker.min.js"></script>
    <script src="/Assets/js/date-time/bootstrap-timepicker.min.js"></script>
    <!--script src="/Assets/js/date-time/moment.min.js"></script-->
    <script src="/Assets/js/date-time/daterangepicker.min.js"></script>
    <!--script src="/Assets/js/bootstrap-colorpicker.min.js"></script-->
    <!--script src="/Assets/js/jquery.knob.min.js"></script-->
    <!--script src="/Assets/js/jquery.autosize.min.js"></script-->
    <!--script src="/Assets/js/jquery.inputlimiter.1.3.1.min.js"></script-->
    <script src="/Assets/js/jquery.maskedinput.min.js"></script>
    <!--script src="/Assets/js/bootstrap-tag.min.js"></script-->
    <script src="/Assets/js/jquery.dataTables.min.js"></script>
    <script src="/Assets/js/jquery.dataTables.bootstrap.js"></script>
    <!--script src="/Assets/js/jquery.nestable.min.js"></script-->
    <script src="/Assets/js/jquery.colorbox-min.js"></script>

    <!-- ace scripts -->

    <!--for Dashboar -->
    <script src="/Assets/js/uncompressed/flot/jquery.flot.js"></script>
    <script src="/Assets/js/uncompressed/flot/jquery.flot.time.js"></script>
    <script src="/Assets/js/uncompressed/flot/jquery.flot.pie.js"></script>
    <script src="/Assets/js/uncompressed/flot/jquery.flot.resize.js"></script>

    <script src="/Assets/js/jqvmap/jquery.vmap.js" type="text/javascript"></script>
    <script src="/Assets/js/jqvmap/maps/jquery.vmap.world.js" type="text/javascript"></script>
    <!--for Dashboar -->

    <script src="/Assets/js/uncompressed/ace-elements.js"></script>
    <script src="/Assets/js/uncompressed/ace.js"></script>


    <script src="/Assets/js/angular.min.js"></script>
    <script src="/Assets/js/angular-route.js"></script>
    <script src="/Assets/Utility.js"></script>
    
    
    <script src="/Staff/JS/app.js"></script>
    <script src="/Staff/JS/controllers.js"></script>

    <script src="/Staff/JS/entityService.js"></script>
    <script src="/Staff/JS/directives.js"></script>
    
    
    <div class="modal"></div>
</body>
</html>
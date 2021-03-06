<!DOCTYPE html>
<html>
<head>
    <title>@ViewBag.Title</title>
    <link href="/assets/css/application.min.css" rel="stylesheet">
    <!-- as of IE9 cannot parse css files with more that 4K classes separating in two files -->
    <!--[if IE 9]>
        <link href="/assets/css/application-ie9-part2.css" rel="stylesheet">
    <![endif]-->
    <link rel="shortcut icon" href="/assets/img/favicon.png">
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no">
    <meta name="description" content="">
    <meta name="author" content="">
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
    <script>
        /* yeah we need this empty stylesheet here. It's cool chrome & chromium fix
         chrome fix https://code.google.com/p/chromium/issues/detail?id=167083
         https://code.google.com/p/chromium/issues/detail?id=332189
         */
    </script>
   <script>
      
        var apiUrl = "http://localhost:22326/";
        //var apiUrl = "http://identity.kuaforx.com/";
        var user = window.localStorage.getItem("apiUser");
        user = JSON.parse(user);
        var Token = user.jwt.token;

    </script>
</head>
<body ng-app="myApp">
    <!--
      Main sidebar seen on the left. may be static or collapsing depending on selected state.

        * Collapsing - navigation automatically collapse when mouse leaves it and expand when enters.
        * Static - stays always open.
    -->
    <nav id="sidebar" class="sidebar" role="navigation">
        <!-- need this .js class to initiate slimscroll -->
        <div class="js-sidebar-content">
            <header class="logo hidden-sm-down">
                <a href="/assets/index.html">sing</a>
            </header>
            <!-- seems like lots of recent admin template have this feature of user info in the sidebar.
                 looks good, so adding it and enhancing with notifications -->
            <div class="sidebar-status hidden-md-up" ng-controller="loginCtrl">
                <a href="#" class="dropdown-toggle" data-toggle="dropdown">
                    <span class="thumb-sm avatar pull-xs-right">
                        <img class="img-circle" src="/assets/demo/img/people/a5.jpg" alt="...">
                    </span>
                    <!-- .circle is a pretty cool way to add a bit of beauty to raw data.
                         should be used with bg-* and text-* classes for colors -->
                    <span class="circle bg-warning fw-bold text-gray-dark">
                        13
                    </span>
                    &nbsp;
                    {{Name}} <strong>{{Surname}}</strong>
                    <b class="caret"></b>
                </a>
                <!-- #notifications-dropdown-menu goes here when screen collapsed to xs or sm -->
            </div>
            <!-- main notification links are placed inside of .sidebar-nav -->
            <ul class="sidebar-nav">
                <li class="active">
                    <!-- an example of nested submenu. basic bootstrap collapse component -->
                    <a href="#sidebar-dashboard" data-toggle="collapse" data-parent="#sidebar">
                        <span class="icon">
                            <i class="fa fa-desktop"></i>
                        </span>
                        Dashboard
                        <i class="toggle fa fa-angle-down"></i>
                    </a>
                    <ul id="sidebar-dashboard" class="collapse in">
                        <li class="active"><a href="/assets/index.html">Dashboard</a></li>
                        <li><a href="/assets/widgets.html">Widgets</a></li>
                    </ul>
                </li>
                <li>
                    <a href="/assets/inbox.html">
                        <span class="icon">
                            <i class="fa fa-envelope"></i>
                        </span>
                        Email
                        <span class="label label-danger">
                            9
                        </span>
                    </a>
                </li>
                <li>
                    <a href="/assets/charts.html">
                        <span class="icon">
                            <i class="glyphicon glyphicon-stats"></i>
                        </span>
                        Charts
                    </a>
                </li>
                <li>
                    <a href="/assets/profile.html">
                        <span class="icon">
                            <i class="glyphicon glyphicon-user"></i>
                        </span>
                        Profile
                        <sup class="text-warning fw-semi-bold">
                            new
                        </sup>
                    </a>
                </li>
            </ul>
            <!-- every .sidebar-nav may have a title -->
            <h5 class="sidebar-nav-title">Template <a class="action-link" href="#"><i class="glyphicon glyphicon-refresh"></i></a></h5>
            <ul class="sidebar-nav">

                <li>
                    <a href="#"  onclick="window.location='/Users/index.php'">
                        <span class="icon">
                            <i class="fa fa-users"></i>
                        </span>
                        Users
                    </a>
                </li>

               <li>
                    <a href="#"  onclick="window.location='/Roles/'">
                        <span class="icon">
                            <i class="fa fa-list"></i>
                        </span>
                        Roles
                    </a>
                </li>


                <li>
                    <a href="#"  onclick="window.location='/Permissions/'">
                        <span class="icon">
                            <i class="fa fa-list"></i>
                        </span>
                        Permissions
       
                    </a>
                </li>



                <li>
                    <!-- an example of nested submenu. basic bootstrap collapse component -->
                    <a class="collapsed" href="#sidebar-forms" data-toggle="collapse" data-parent="#sidebar">
                        <span class="icon">
                            <i class="glyphicon glyphicon-align-right"></i>
                        </span>
                        Forms
                        <i class="toggle fa fa-angle-down"></i>
                    </a>
                    <ul id="sidebar-forms" class="collapse">
                        <li><a href="/assets/form_elements.html">Form Elements</a></li>
                        <li><a href="/assets/form_validation.html">Form Validation</a></li>
                        <li><a href="/assets/form_wizard.html">Form Wizard</a></li>
                    </ul>
                </li>
                <li>
                    <a class="collapsed" href="#sidebar-ui" data-toggle="collapse" data-parent="#sidebar">
                        <span class="icon">
                            <i class="glyphicon glyphicon-tree-conifer"></i>
                        </span>
                        UI Elements
                        <i class="toggle fa fa-angle-down"></i>
                    </a>
                    <ul id="sidebar-ui" class="collapse">
                        <li><a href="ui_components.html">Components</a></li>
                        <li><a href="ui_notifications.html">Notifications</a></li>
                        <li><a href="ui_icons.html">Icons</a></li>
                        <li><a href="ui_buttons.html">Buttons</a></li>
                        <li><a href="ui_tabs_accordion.html">Tabs & Accordion</a></li>
                        <li><a href="ui_list_groups.html">List Groups</a></li>
                    </ul>
                </li>
                <li>
                    <a href="/grid.html">
                        <span class="icon">
                            <i class="glyphicon glyphicon-th"></i>
                        </span>
                        Grid
                    </a>
                </li>
                <li>
                    <a class="collapsed" href="#sidebar-maps" data-toggle="collapse" data-parent="#sidebar">
                        <span class="icon">
                            <i class="glyphicon glyphicon-map-marker"></i>
                        </span>
                        Maps
                        <i class="toggle fa fa-angle-down"></i>
                    </a>
                    <ul id="sidebar-maps" class="collapse">
                        <!-- data-no-pjax turns off pjax loading for this link. Use in case of complicated js loading on the
                             target page -->
                        <li><a href="/assets/maps_google.html" data-no-pjax>Google Maps</a></li>
                        <li><a href="/assets/maps_vector.html">Vector Maps</a></li>
                    </ul>
                </li>
                <li>
                    <!-- an example of nested submenu. basic bootstrap collapse component -->
                    <a class="collapsed" href="#sidebar-tables" data-toggle="collapse" data-parent="#sidebar">
                        <span class="icon">
                            <i class="fa fa-table"></i>
                        </span>
                        Tables
                        <i class="toggle fa fa-angle-down"></i>
                    </a>
                    <ul id="sidebar-tables" class="collapse">
                        <li><a href="/assets/tables_basic.html">Tables Basic</a></li>
                        <li><a href="/assets/tables_dynamic.html">Tables Dynamic</a></li>
                    </ul>
                </li>
                <li>
                    <a class="collapsed" href="#sidebar-extra" data-toggle="collapse" data-parent="#sidebar">
                        <span class="icon">
                            <i class="fa fa-leaf"></i>
                        </span>
                        Extra
                        <i class="toggle fa fa-angle-down"></i>
                    </a>
                    <ul id="sidebar-extra" class="collapse">
                        <li><a href="/assets/calendar.html">Calendar</a></li>
                        <li><a href="/assets/invoice.html">Invoice</a></li>
                        <li><a href="/assets/login.html" target="_blank" data-no-pjax>Login Page</a></li>
                        <li><a href="/assets/error.html" target="_blank" data-no-pjax>Error Page</a></li>
                        <li><a href="/assets/gallery.html">Gallery</a></li>
                        <li><a href="/assets/search.html">Search Results</a></li>
                        <li><a href="/assets/time_line.html" data-no-pjax>Time Line</a></li>
                    </ul>
                </li>
                <li>
                    <a class="collapsed" href="#sidebar-levels" data-toggle="collapse" data-parent="#sidebar">
                        <span class="icon">
                            <i class="fa fa-folder-open"></i>
                        </span>
                        Menu Levels
                        <i class="toggle fa fa-angle-down"></i>
                    </a>
                    <ul id="sidebar-levels" class="collapse">
                        <li><a href="#">Level 1</a></li>
                        <li>
                            <a class="collapsed" href="#sidebar-sub-levels" data-toggle="collapse" data-parent="#sidebar-levels">
                                Level 2
                                <i class="toggle fa fa-angle-down"></i>
                            </a>
                            <ul id="sidebar-sub-levels" class="collapse">
                                <li><a href="#">Level 3</a></li>
                                <li><a href="#">Level 3</a></li>
                            </ul>
                        </li>
                    </ul>
                </li>
            </ul>
            <h5 class="sidebar-nav-title">Labels <a class="action-link" href="#"><i class="glyphicon glyphicon-plus"></i></a></h5>
            <!-- some styled links in sidebar. ready to use as links to email folders, projects, groups, etc -->
            <ul class="sidebar-labels">
                <li>
                    <a href="#">
                        <!-- yep, .circle again -->
                        <i class="fa fa-circle text-warning mr-xs"></i>
                        <span class="label-name">My Recent</span>
                    </a>
                </li>
                <li>
                    <a href="#">
                        <i class="fa fa-circle text-gray mr-xs"></i>
                        <span class="label-name">Starred</span>
                    </a>
                </li>
                <li>
                    <a href="#">
                        <i class="fa fa-circle text-danger mr-xs"></i>
                        <span class="label-name">Background</span>
                    </a>
                </li>
            </ul>
            <h5 class="sidebar-nav-title">Projects</h5>
            <!-- A place for sidebar notifications & alerts -->
            <div class="sidebar-alerts">
                <div class="alert fade in">
                    <a href="#" class="close" data-dismiss="alert" aria-hidden="true">&times;</a>
                    <span class="text-white fw-semi-bold">Sales Report</span> <br>
                    <div class="bg-gray-transparent progress-bar">
                        <progress class="progress progress-xs progress-bar-gray-light mt-xs mb-0" value="100" max="100" style="width: 16%"></progress>
                    </div>
                    <small>Calculating x-axis bias... 65%</small>
                </div>
                <div class="alert fade in">
                    <a href="#" class="close" data-dismiss="alert" aria-hidden="true">&times;</a>
                    <span class="text-white fw-semi-bold">Personal Responsibility</span> <br>
                    <div class="bg-gray-transparent progress-bar">
                        <progress class="progress progress-xs progress-danger mt-xs mb-0" value="100" max="100" style="width: 23%"></progress>
                    </div>
                    <small>Provide required notes</small>
                </div>
            </div>
        </div>
    </nav>
    <!-- This is the white navigation bar seen on the top. A bit enhanced BS navbar. See .page-controls in _base.scss. -->
    <nav class="page-controls navbar navbar-dashboard">
        <div class="container-fluid">
            <!-- .navbar-header contains links seen on xs & sm screens -->
            <div class="navbar-header">
                <ul class="nav navbar-nav">
                    <li class="nav-item">
                        <!-- whether to automatically collapse sidebar on mouseleave. If activated acts more like usual admin templates -->
                        <a class="hidden-md-down nav-link" id="nav-state-toggle" href="#" data-toggle="tooltip" data-html="true" data-original-title="Turn<br>on/off<br>sidebar<br>collapsing" data-placement="bottom">
                            <i class="fa fa-bars fa-lg"></i>
                        </a>
                        <!-- shown on xs & sm screen. collapses and expands navigation -->
                        <a class="hidden-lg-up nav-link" id="nav-collapse-toggle" href="#" data-html="true" title="Show/hide<br>sidebar" data-placement="bottom">
                            <span class="rounded rounded-lg bg-gray text-white hidden-md-up"><i class="fa fa-bars fa-lg"></i></span>
                            <i class="fa fa-bars fa-lg hidden-sm-down"></i>
                        </a>
                    </li>
                    <li class="nav-item hidden-sm-down"><a href="#" class="nav-link"><i class="fa fa-refresh fa-lg"></i></a></li>
                    <li class="nav-item ml-n-xs hidden-sm-down"><a href="#" class="nav-link"><i class="fa fa-times fa-lg"></i></a></li>
                </ul>
                <ul class="nav navbar-nav navbar-right hidden-md-up">
                    <li>
                        <!-- toggles chat -->
                        <a href="#" data-toggle="chat-sidebar">
                            <span class="rounded rounded-lg bg-gray text-white"><i class="fa fa-globe fa-lg"></i></span>
                        </a>
                    </li>
                </ul>
                <!-- xs & sm screen logo -->
                <a class="navbar-brand hidden-md-up" href="/assets/index.html">
                    <i class="fa fa-circle text-gray mr-n-sm"></i>
                    <i class="fa fa-circle text-warning"></i>
                    &nbsp;
                    sing
                    &nbsp;
                    <i class="fa fa-circle text-warning mr-n-sm"></i>
                    <i class="fa fa-circle text-gray"></i>
                </a>
            </div>

            <!-- this part is hidden for xs screens -->
            <div class="collapse navbar-collapse"  ng-controller="loginCtrl">
                <!-- search form! link it to your search server -->
                <form class="navbar-form pull-xs-left" role="search">
                    <div class="form-group">
                        <div class="input-group input-group-no-border">
                            <span class="input-group-addon">
                                <i class="fa fa-search"></i>
                            </span>
                            <input class="form-control" type="text" placeholder="Search Dashboard">
                        </div>
                    </div>
                </form>
                <ul class="nav navbar-nav pull-xs-right">
                    <li class="dropdown nav-item">
                        <a href="#" class="dropdown-toggle dropdown-toggle-notifications nav-link" id="notifications-dropdown-toggle" data-toggle="dropdown">
                            <span class="thumb-sm avatar pull-xs-left">
                                <img class="img-circle" src="/assets/demo/img/people/a5.jpg" alt="...">
                            </span>
                            &nbsp;
                            {{Name}} <strong>{{Surname}}</strong>&nbsp;
                            <span class="circle bg-warning fw-bold">
                                13
                            </span>
                            <b class="caret"></b>
                        </a>
                        <!-- ready to use notifications dropdown.  inspired by smartadmin template.
                             consists of three components:
                             notifications, messages, progress. leave or add what's important for you.
                             uses Sing's ajax-load plugin for async content loading. See #load-notifications-btn -->
                        <div class="dropdown-menu dropdown-menu-right animated fadeInUp" id="notifications-dropdown-menu">
                            <section class="card notifications">
                                <header class="card-header">
                                    <div class="text-xs-center mb-sm">
                                        <strong>You have 13 notifications</strong>
                                    </div>
                                    <div class="btn-group btn-group-sm btn-group-justified" id="notifications-toggle" data-toggle="buttons">
                                        <label class="btn btn-secondary active">
                                            <!-- ajax-load plugin in action. setting data-ajax-load & data-ajax-target is the
                                                 only requirement for async reloading -->
                                            <input type="radio" checked
                                                   data-ajax-trigger="change"
                                                   data-ajax-load="demo/ajax/notifications.html"
                                                   data-ajax-target="#notifications-list"> Notifications
                                        </label>
                                        <label class="btn btn-secondary">
                                            <input type="radio"
                                                   data-ajax-trigger="change"
                                                   data-ajax-load="demo/ajax/messages.html"
                                                   data-ajax-target="#notifications-list"> Messages
                                        </label>
                                        <label class="btn btn-secondary">
                                            <input type="radio"
                                                   data-ajax-trigger="change"
                                                   data-ajax-load="demo/ajax/progress.html"
                                                   data-ajax-target="#notifications-list"> Progress
                                        </label>
                                    </div>
                                </header>
                                <!-- notification list with .thin-scroll which styles scrollbar for webkit -->
                                <div id="notifications-list" class="list-group thin-scroll">
                                    <div class="list-group-item">
                                        <span class="thumb-sm pull-xs-left mr clearfix">
                                            <img class="img-circle" src="/assets/demo/img/people/a3.jpg" alt="...">
                                        </span>
                                        <p class="no-margin overflow-hidden">
                                            1 new user just signed up! Check out
                                            <a href="#">Monica Smith</a>'s account.
                                            <time class="help-block no-margin">
                                                2 mins ago
                                            </time>
                                        </p>
                                    </div>
                                    <a class="list-group-item" href="#">
                                        <span class="thumb-sm pull-xs-left mr">
                                            <i class="glyphicon glyphicon-upload fa-lg"></i>
                                        </span>
                                        <p class="text-ellipsis no-margin">
                                            2.1.0-pre-alpha just released.
                                        </p>
                                        <time class="help-block no-margin">
                                            5h ago
                                        </time>
                                    </a>
                                    <a class="list-group-item" href="#">
                                        <span class="thumb-sm pull-xs-left mr">
                                            <i class="fa fa-bolt fa-lg"></i>
                                        </span>
                                        <p class="text-ellipsis no-margin">
                                            Server load limited.
                                        </p>
                                        <time class="help-block no-margin">
                                            7h ago
                                        </time>
                                    </a>
                                    <div class="list-group-item">
                                        <span class="thumb-sm pull-xs-left mr clearfix">
                                            <img class="img-circle" src="/assets/demo/img/people/a5.jpg" alt="...">
                                        </span>
                                        <p class="no-margin overflow-hidden">
                                            User <a href="#">Jeff</a> registered
                                            &nbsp;&nbsp;
                                            <a class="label label-success">Allow</a>
                                            <a class="label label-danger">Deny</a>
                                            <time class="help-block no-margin">
                                                12:18 AM
                                            </time>
                                        </p>
                                    </div>
                                    <div class="list-group-item">
                                        <span class="thumb-sm pull-xs-left mr">
                                            <i class="fa fa-shield fa-lg"></i>
                                        </span>
                                        <p class="no-margin overflow-hidden">
                                            Instructions for changing your Envato Account password. Please
                                            check your account <a href="#">security page</a>.
                                            <time class="help-block no-margin">
                                                12:18 AM
                                            </time>
                                        </p>
                                    </div>
                                    <a class="list-group-item" href="#">
                                        <span class="thumb-sm pull-xs-left mr">
                                            <span class="rounded bg-primary rounded-lg">
                                                <i class="fa fa-facebook text-white"></i>
                                            </span>
                                        </span>
                                        <p class="text-ellipsis no-margin">
                                            New <strong>76</strong> facebook likes received.
                                        </p>
                                        <time class="help-block no-margin">
                                            15 Apr 2014
                                        </time>
                                    </a>
                                    <a class="list-group-item" href="#">
                                        <span class="thumb-sm pull-xs-left mr">
                                            <span class="circle circle-lg bg-gray-dark">
                                                <i class="fa fa-circle-o text-white"></i>
                                            </span>
                                        </span>
                                        <p class="text-ellipsis no-margin">
                                            Dark matter detected.
                                        </p>
                                        <time class="help-block no-margin">
                                            15 Apr 2014
                                        </time>
                                    </a>
                                </div>
                                <footer class="card-footer text-sm">
                                    <!-- ajax-load button. loads demo/ajax/notifications.php to #notifications-list
                                         when clicked -->
                                    <button class="btn-label btn-link pull-xs-right"
                                            id="load-notifications-btn"
                                            data-ajax-load="demo/ajax/notifications.php"
                                            data-ajax-target="#notifications-list"
                                            data-loading-text="<i class='fa fa-refresh fa-spin mr-xs'></i> Loading...">
                                        <i class="fa fa-refresh"></i>
                                    </button>
                                    <span>Synced at: 21 Apr 2014 18:36</span>
                                </footer>
                            </section>
                        </div>
                    </li>
                    <li class="dropdown nav-item">
                        <a href="#" class="dropdown-toggle nav-link" data-toggle="dropdown">
                            <i class="fa fa-cog fa-lg"></i>
                        </a>
                        <ul class="dropdown-menu dropdown-menu-right">
                            <li><a class="dropdown-item" href="/assets/profile.html"><i class="glyphicon glyphicon-user"></i> &nbsp; My Account</a></li>
                            <li class="dropdown-divider"></li>
                            <li><a class="dropdown-item" href="/assets/calendar.html">Calendar</a></li>
                            <li><a class="dropdown-item" href="/assets/inbox.html">Inbox &nbsp;&nbsp;<span class="label label-pill label-danger animated bounceIn">9</span></a></li>
                            <li class="dropdown-divider"></li>
                            <li><a class="dropdown-item" href="/assets/login.html"><i class="fa fa-sign-out"></i> &nbsp; Log Out</a></li>
                        </ul>
                    </li>
                </ul>
            </div>
        </div>
    </nav>

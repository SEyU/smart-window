<!DOCTYPE html>
<html>
    <head>
        <meta charset="utf-8">
        <meta http-equiv="X-UA-Compatible" content="IE=edge">
        <meta name="viewport" content="width=device-width, initial-scale=1">
        <meta name="description" content="">
        <meta name="author" content="">

        <!-- jQuery -->
        <script src="{{asset('vendor/jquery/jquery.min.js')}}"></script>

        <!-- Bootstrap Core JavaScript -->
        <script src="{{asset('vendor/bootstrap/js/bootstrap.min.js')}}"></script>
            <!-- Bootstrap Core CSS -->
            <link href="{{ asset('vendor/bootstrap/css/bootstrap.min.css') }}" rel="stylesheet">

            <!-- MetisMenu CSS -->
            <link href="{{'vendor/metisMenu/metisMenu.min.css'}}" rel="stylesheet">

            <!-- Custom CSS -->
            <link href="{{asset('css/sb-admin-2.css')}}" rel="stylesheet">

            <link href="{{asset('css/weather-icons.css')}}" rel="stylesheet">
            <link href="{{asset('css/weather-icons-wind.css')}}" rel="stylesheet">


            <!-- Morris Charts CSS -->
            <link href="{{asset('vendor/morrisjs/morris.css')}}" rel="stylesheet">

            <!-- Custom Fonts -->
            <link href="{{asset('vendor/font-awesome/css/font-awesome.min.css')}}" rel="stylesheet" type="text/css">

            <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
            <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
            <!--[if lt IE 9]>
                <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
                <script src="https://oss.maxcdn.com/libs/respond.js/1.4.2/respond.min.js"></script>
            <![endif]-->

        <title>@yield('title', 'Smart Windows')</title>

    </head>
    <body>


        <div  id="wraper">
            <!-- Navigation -->
            <nav class="navbar navbar-default navbar-static-top" role="navigation" style="margin-bottom: 0">

                <div class="navbar-header">
                    <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
                        <span class="sr-only">Toggle navigation</span>
                        <span class="icon-bar"></span>
                        <span class="icon-bar"></span>
                        <span class="icon-bar"></span>
                    </button>
                    <a class="navbar-brand" href="{{url('/')}}">Smart Window</a>
                </div>
                <!-- /.navbar-header -->


                <div class="navbar-default sidebar" role="navigation">
                    <div class="sidebar-nav navbar-collapse">
                        <ul class="nav" id="side-menu">
                            <li>
                                <a href="{{url('/')}}"><i class="fa fa-dashboard fa-fw"></i> Dashboard</a>
                            </li>
                            <li>
                                <a href="{{url('/temperatura')}}"><i class="wi wi-thermometer fa-fw"></i> Temperatura</a>
                            </li>
                            <li>
                                <a href="{{url('/viento')}}"><i class="wi wi-strong-wind fa-fw"></i> Viento</a>
                            </li>
                        </ul>
                    </div>
                    <!-- /.sidebar-collapse -->
                </div>
                <!-- /.navbar-static-side -->

            </nav>

            <div id="page-wrapper">
                <div class="row">
                    <div class="col-lg-12">
                        <h1 class="page-header">@yield('title')</h1>
                    </div>
                    <!-- /.col-lg-12 -->
                </div>
                <div class="row">
                    @yield('content')

                </div>

        </div>

        <!-- /#wrapper -->



        <!-- Metis Menu Plugin JavaScript -->
        <script src="{{asset('vendor/metisMenu/metisMenu.min.js')}}"></script>

        @yield('snippets-scripts')



        <!-- Custom Theme JavaScript -->
        <script src="{{asset('js/sb-admin-2.js')}}"></script>



    </body>
</html>

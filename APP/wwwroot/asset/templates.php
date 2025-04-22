<?php 
date_default_timezone_set('America/Managua');
$Roll= $_SESSION['roll'];
 ?>
<!DOCTYPE html>
<!--
This is a starter template page. Use this page to start your new project from
scratch. This page gets rid of all links and provides the needed markup only.
-->
<html lang="es">
<head>
  <meta charset="utf-8">
  <meta name="viewport" content="width=device-width, initial-scale=1">
  <meta http-equiv="x-ua-compatible" content="ie=edge">

  <title>ControlPOS | Cootel</title>

  <!-- Font Awesome Icons -->
  <link rel="icon" type="image/png" href="Views/dist/img/logcootel.ico"/>
  <link rel="stylesheet" href="Views/plugins/fontawesome-free/css/all.min.css">
  <!-- Theme style -->
  <link rel="stylesheet" href="Views/dist/css/adminlte.min.css">
  <!-- Google Font: Source Sans Pro -->
  <link href="https://fonts.googleapis.com/css?family=Source+Sans+Pro:300,400,400i,700" rel="stylesheet">
  <link rel="stylesheet" type="text/css" href="Controllers/alertifyjs/css/alertify.css">
  <link rel="stylesheet" type="text/css" href="Controllers/alertifyjs/css/themes/default.css">
  <script src="Controllers/alertifyjs/alertify.js"></script>
</head>
<body class="hold-transition sidebar-mini">
<div class="wrapper">

 <?php require_once 'Views/navbar.html'; ?>

 <?php if ($Roll == 3) {
     require_once 'Views/sliderbar.php'; 
   } else{
    require_once 'Views/sliderbar_ventas.php'; 
   }
  ?>
  <!-- Content Wrapper. Contains page content -->
  <div class="content-wrapper">
    <!-- Content Header (Page header) -->
    <div class="content-header">
      <div class="container-fluid">
        <div class="row mb-2">
          <div class="col-sm-6">
            <h1 class="m-0 text-dark">Control POS</h1>
          </div><!-- /.col -->
          <div class="col-sm-6">
            <ol class="breadcrumb float-sm-right">
              <li class="breadcrumb-item"><a href="#">Inicio</a></li>
              <li class="breadcrumb-item active">FeedBack</li>
            </ol>
          </div><!-- /.col -->
        </div><!-- /.row -->
      </div><!-- /.container-fluid -->
    </div>
    <!-- /.content-header -->
<form method="POST" id="frm_feed" name="frm_feed" enctype="multipart/form-data">
  
    <!-- Main content -->
    <div class="content">
      <?php require_once 'Views/informacion.php'; ?>
      <div class="container-fluid">
        <div class="row">
          <div class="col-lg-6">
            <div class="card">
              <div class="card-header">
                <h5 class="m-0" style="color: orange">Recursos humanos</h5>
              </div>
              <div class="card-body">
                <?php require_once 'recursos_humanos.html'; ?>
              </div>
            </div>

            <div class="card">
              <div class="card-header">
                <h5 class="m-0" style="color: orange">Personal de venta en calle</h5>
              </div>
              <?php require_once 'Views/personal_venta.html' ?>
            </div><!-- /.card -->
          </div>
          <!-- /.col-md-6 -->
          <div class="col-lg-6">
            <div class="card">
              <div class="card-header">
                <h5 class="m-0" style="color:orange">Establecimiento</h5>
              </div>
              <div class="card-body">
                <?php require_once 'Views/establecimiento.html'; ?>
              </div>
            </div>

            <div class="card">
              <div class="card-header">
                <h5 class="m-0" style="color:orange">Logistica</h5>
              </div>
              <?php require_once 'Views/logistica.html'; ?>
            </div>
          </div> 
          <!-- /.col-md-6 -->
          
        </div>
        <!-- /.row -->
      </div><!-- /.container-fluid -->
    </div>
    <!-- /.content -->
  </div>
  <!-- /.content-wrapper -->

  <div class="content-wrapper">
    <!-- Content Header (Page header) --> <!-- inicia segundo bloque -->
   

    <!-- Main content -->
    <div class="content">
      <div class="container-fluid">
        <div class="row">
          <div class="col-lg-6">
            <div class="card">
              <div class="card-header">
                <h5 class="m-0" style="color: orange">Inventario disponible del distribuidor</h5>
              </div>
              <div class="card-body">
                <?php require_once 'inventario_disponible.html'; ?>
              </div>
            </div>

            <div class="card">
              <div class="card-header">
                <h5 class="m-0" style="color: orange">Evaluación de tecnicas de ventas</h5>
              </div>
              <?php require_once 'evaluacion.html'; ?>
            </div><!-- /.card -->
          </div>
          <!-- /.col-md-6 -->
          <div class="col-lg-6">
            <div class="card">
              <div class="card-header">
                <h5 class="m-0" style="color:orange">Gestiones realizadas por el agente Cootel</h5>
              </div>
              <div class="card-body">
                <?php require_once 'gestiones_agente_cootel.html'; ?>
              </div>
            </div>

            <div class="card">
              <div class="card-header">
                <h5 class="m-0" style="color:orange">Informacion general</h5>
              </div>
              <?php require_once 'info_general.html'; ?>
            </div>
          </div> 
          <!-- /.col-md-6 -->
					<div class="col-sm-offset-10 col-sm-10">
						<button id="btn_reg" type="submit" class="btn btn-warning" name="btn_registrar" style="color:white; background: orange; border-radius: 20px">Registrar visita</button>
          </div>
          <legend>  </legend>
        </div>
        <!-- /.row -->
      </div><!-- /.container-fluid -->
    </div>
    <!-- /.content -->
  </div>
  <!-- /.content-wrapper -->   <!-- segundo bloque --> 
</form>
  <!-- Control Sidebar -->
  <aside class="control-sidebar control-sidebar-dark">
    <!-- Control sidebar content goes here -->
    <div class="p-3">
      <h5>Title</h5>
      <p>Sidebar content</p>
    </div>
  </aside>
  <!-- /.control-sidebar -->

  <!-- Main Footer -->
  <?php require_once 'footer.html' ?>
</div>
<!-- ./wrapper -->

<!-- REQUIRED SCRIPTS -->

<!-- jQuery -->
<script src="Views/plugins/jquery/jquery.min.js"></script>
<!-- Bootstrap 4 -->
<script src="Views/plugins/bootstrap/js/bootstrap.bundle.min.js"></script>
<!-- AdminLTE App -->
<script src="Views/dist/js/adminlte.min.js"></script>
<script src="Controllers/notificacion.js"></script>
<script src="./Controllers/consulta_dist.js"></script>
<script src="Controllers/localizacion.js"></script>
<script src="Controllers/registrar_feedback.js"></script>
<script src="Controllers/modelo.js"></script>
</body>
</html>

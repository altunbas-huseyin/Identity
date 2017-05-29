<?php include("../header.php"); ?>

<div class="content-wrap" ng-controller="permissionCtrl">
    <!-- main page content. the place to put widgets in. usually consists of .row > .col-lg-* > .widget.  -->
    <main id="content" class="content" role="main">
        <h1 class="page-title">Dashboard <small><small>The Lucky One</small></small></h1>

        <div class="row">

            <div id="grid"></div>

        </div>
    </main>
</div>



<script>
    window.onload = function ()
    {
        //bu örnek kullanıldı
        //http://docs.telerik.com/kendo-ui/framework/datasource/crud
        RunKendo();
    }
    //setTimeout(RunKendo, 5000);
    function RunKendo() 
    {


        
      
       
    }

</script>  

<?php include("../footer.php"); ?>
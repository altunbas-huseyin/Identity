<?php include("../header.php"); ?>

<div class="content-wrap">
    <!-- main page content. the place to put widgets in. usually consists of .row > .col-lg-* > .widget.  -->
    <main id="content" class="content" role="main">
        <h1 class="page-title">Dashboard <small><small>The Lucky One</small></small></h1>

        <div class="row">

            <div id="grid"></div>

        </div>
    </main>
</div>



<script>

   window.onload = function() {
       runKendo();
   }


   function runKendo() {

       var remoteDataSource = new kendo.data.DataSource({
           pageSize: 20,
           transport: {
               read: {
                   url: apiUrl + "api/Permission",
                   dataType: "json",
               },
               create: {
                   url: apiUrl + "api/Permission",
                   dataType: "json",
                   type: "POST",
               },
               update: {
                   url: apiUrl + "api/Permission",
                   dataType: "json",
                   type: "PUT",
               },
               destroy: {
                   url: apiUrl + "api/Permission",
                   dataType: "json",
                   type: "DELETE"
               }
           },
           schema: {
               data: "result",
               model: {
                   id: "_id",
                   fields: {
                       id: {
                           validation: {
                               required: true
                           },
                           editable: false,
                           hidden: true
                       },
                       name: {
                           validation: {
                               required: true
                           }
                       },

                   }
               }
           }
       });

       $('#grid').kendoGrid({
           dataSource: remoteDataSource,
           toolbar: [{
               name: "create",
               text: "Create Customer"
           }],
           editable: "popup",
           scrollable: true,
           sortable: true,
           filterable: true,
           pageable: {
               refresh: true,
               pageSizes: true,
               buttonCount: 5
           },
           columns: [

               {
                   field: "id",
                   title: "Id",
                   hidden: true,
               },
               {
                   field: "name",
                   title: "Name"
               },
               {
                   field: "description",
                   title: "Description"
               },
               {
                   command: ["edit", "destroy"],
                   width: "400px"
               }
           ]
       });
   }

</script>  

<?php include("../footer.php"); ?>
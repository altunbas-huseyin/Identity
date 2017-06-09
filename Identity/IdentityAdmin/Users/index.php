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
                   url: apiUrl + "api/Users",
                   dataType: "json",
               },
               create: {
                   url: apiUrl + "api/Users",
                   dataType: "json",
                   type: "POST",
               },
               update: {
                   url: apiUrl + "api/Users",
                   dataType: "json",
                   type: "PUT",
               },
               destroy: {
                   url: apiUrl + "api/Users",
                   dataType: "json",
                   type: "DELETE"
               }
           },
           schema: {
               data: "result",
               model: {
                   id: "_id",
                   fields: {
                     id: {   editable: false,  hidden: true  },
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

                { field: "id", title: "Id" },
                { field: "name", title: "Name" },
                { field: "surName", title: "SurName" },
                { field: "email", title: "Email" },
                { field: "password", title: "Password" },
                //{ field: "role", title: "Role", template: "#=  JSON.stringify(role) # " },
                { field: "extra1", title: "Extra1" },
                { field: "extra2", title: "Extra2" },
               {
                   command: ["edit", "destroy"],
                   width: "400px"
               }
           ]
       });
   }

   
</script>  

<?php include("../footer.php"); ?>
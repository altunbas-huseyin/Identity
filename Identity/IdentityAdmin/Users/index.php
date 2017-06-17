<?php include("../header.php"); ?>

<div class="content-wrap">
    <!-- main page content. the place to put widgets in. usually consists of .row > .col-lg-* > .widget.  -->
    <main id="content" class="content" role="main">
        <h1 class="page-title">User <small><small>Crud Page</small></small></h1>

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
               text: "Create User"
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
                { field: "extra1", title: "Extra1" },
                { field: "extra2", title: "Extra2" },
                { field: "id", title: "id", width:"190px", template: "<a class='k-button k-button-icontext' target='_blank' href='/Users/userRole.php?_id=#=id#'> Role Manager </a>" },
                {
                   command: ["edit", "destroy"],
                   width: "250px"
                }
           ]
       });
   }

   
</script>  

<?php include("../footer.php"); ?>
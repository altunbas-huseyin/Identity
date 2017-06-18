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

<?php $roleId = $_GET['roleId']; ?>


<script>
   
function onChange() {
    debugger;
    var ddl = $("#name").data("kendoDropDownList");
    var id = ddl.element.val();
    $('#grid').data('kendoGrid').dataSource.transport.options.create.url = apiUrl + "api/RolePermission/<?=$roleId?>/"+id;
    
}

   window.onload = function() {
       runKendo();
   }


   function runKendo() {

       var remoteDataSource = new kendo.data.DataSource({
           pageSize: 20,
           transport: {
               read: {
                   url: apiUrl + "api/RolePermission/<?=$roleId?>",
                   dataType: "json",
               },
               create: {
                   url: apiUrl + "",
                   dataType: "json",
                   type: "POST",
               },
               //update: {
               //    url: apiUrl + "api/Users",
               //    dataType: "json",
               //    type: "PUT",
               //},
               destroy: {
                   url: apiUrl + "api/RolePermission/",
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
               text: "Create Role Permission"
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
                { field: "permissionId", title: "permissionId" },
                { field: "name", title: "name", width: "180px", editor: categoryDropDownEditor, template: "#=name#" },
                { field: "description", title: "Description" },
               //{
                //   command: ["destroy"],//edit butonu olmayacak kullanıcıya sadece rol ekleme ve silme olacak
                //   width: "400px"
               //},
               {
                        command: [
                                      { text: "Delete", click: rolePermissionDelete }
                                 ],  title: "Delete", width: "10%"
                    },

           ]
       });
   }

   

                function categoryDropDownEditor(container, options) {
                    $('<input required name="' + options.field + '" id="' + options.field + '"/>')
                        .appendTo(container)
                        .kendoDropDownList({
                            dataTextField: "name",
                            dataValueField: "_id",
                            autoBind: true,
                            change: onChange,
                            dataSource: new kendo.data.DataSource({
                                schema: {
                                    model: {
                                        //id: "_id"
                                    }
                                },
                                transport: {
                                    read: {
                                        url: apiUrl + "api/Permissions",
                                        dataType: "json",
                                        data: {
                                           // _id: _id
                                        }
                                    }
                                },
                                schema: {
                                          data: "result",
                                        }
                            })
                            
                        });
                }



  function rolePermissionDelete(e) {
            e.preventDefault();
            //Get Data Info
            var dataItem = this.dataItem($(e.currentTarget).closest("tr"));
            console.log(dataItem);
            
            
            swal.queue([{
                title: 'Confirm',
                confirmButtonText: 'Delete Record',
                text:
                    'is confirm?' ,
                showLoaderOnConfirm: true,
                preConfirm: function () {
                    return new Promise(function (resolve) {
                         
                         $.ajax({
                            type: "DELETE",
                            url: apiUrl + "api/RolePermission/"+dataItem._id,
                            ajaxasync: true,
                            data: {  },
                            success: function (data) {
                               if(data.status)
                               {
                                   swal("Succes");
                                   runKendo();
                               }
                            },
                            error: function (data) {
                               
                            }
                        })
                        
                    })
                }
                }])


  }

</script>  

<?php include("../footer.php"); ?>
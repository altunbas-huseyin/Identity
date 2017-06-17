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

<?php $_id = $_GET['_id']; ?>

<script>
   
var roleId="pppp";
function onChange() {
    var ddl = $("#name").data("kendoDropDownList");
    var id = ddl.element.val();
    roleId = id;

    $('#grid').data('kendoGrid').dataSource.transport.options.create.url = apiUrl + "api/UserRole/<?=$_id?>/"+roleId;
}

   window.onload = function() {
       runKendo();
   }


   function runKendo() {

       var remoteDataSource = new kendo.data.DataSource({
           pageSize: 20,
           transport: {
               read: {
                   url: apiUrl + "api/UserRole/<?=$_id?>",
                   dataType: "json",
               },
               create: {
                   url: "",
                   dataType: "json",
                   type: "POST",
               },
               //update: {
               //    url: apiUrl + "api/Users",
               //    dataType: "json",
               //    type: "PUT",
               //},
               destroy: {
                   url: apiUrl + "api/UserRole",
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
               text: "Create Role"
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
                //{ field: "name", title: "Name" },
                 { field: "name", title: "name", width: "180px", editor: categoryDropDownEditor, template: "#=name#" },
                { field: "description", title: "Description" },

               //{
                //   command: ["destroy"],//edit butonu olmayacak kullanıcıya sadece rol ekleme ve silme olacak
                //   width: "400px"
               //},
               {
                        command: [
                                      { text: "Delete", click: roleDelete }
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
                                        url: apiUrl + "api/Roles",
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



  function roleDelete(e) {
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
                            url: apiUrl + "api/UserRole/<?=$_id?>/"+dataItem._id,
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
<?php include("../header.php"); ?>

<div class="content-wrap">
    <!-- main page content. the place to put widgets in. usually consists of .row > .col-lg-* > .widget.  -->
    <main id="content" class="content" role="main">
        <h1 class="page-title">Dashboard <small><small>The Lucky One</small></small></h1>

        <div class="row">

            <div id="grid1"></div>

        </div>
    </main>
</div>



<script>
    window.onload = function ()
    {
        RunKendo();
    }
    //setTimeout(RunKendo, 5000);
    function RunKendo() {
        dataSource = new kendo.data.DataSource({
            transport: {
                read:
                {
                    url: apiUrl + "api/Users",
                    dataType: "json",
                    contentType: "application/json",
                    type: "GET",
                    beforeSend: function (req) {
                        req.setRequestHeader('Token', Token);
                        req.setRequestHeader('api-version', '1.0');
                    }
                },
                destroy:
                {
                    url: apiUrl + "api/Users",
                    type: "DELETE",
                    beforeSend: function (req) {
                        req.setRequestHeader('Token', Token);
                        req.setRequestHeader('api-version', '1.0');
                    }
                },
                create:
                {
                    url: apiUrl + "api/Users",
                    type: "POST",
                    beforeSend: function (req) {
                        req.setRequestHeader('Token', Token);
                        req.setRequestHeader('api-version', '1.0');
                    }
                },
                update:
                {
                    url: apiUrl + "api/Users",
                    type: "PUT",
                    parameterMap: function (options, operation) {
                        if (operation !== "read" && options.models) {
                            return {
                                models: kendo.stringify(options.models)
                            };
                        }
                    },
                    beforeSend: function (req) {
                        req.setRequestHeader('Token', Token);
                        req.setRequestHeader('api-version', '1.0');
                    }
                },
            },
            schema:
            {
                data: "result",
                model:
                {
                    id: "_id",
                    fields: {
                        name: { editable: false, nullable: true, type: "string" },
                        surName: { editable: true, nullable: true, type: "string" },
                    }
                }
            }
        });
        $("#grid1").kendoGrid({
            dataSource: dataSource,
            //editable: "inline",
            toolbar: ["create"],
            columns: [
                { field: "_id", title: "Id", },
                { field: "name", title: "Name" },
                { field: "surName", title: "SurName" },
                { field: "email", title: "Email" },
                { field: "status", title: "Status" },
                { field: "role", title: "Role" },
                //{ field: "role", title: "Role", template: "#=  JSON.stringify(role) # " },
                { field: "extra1", title: "Extra1" },
                { field: "extra2", title: "Extra2" },
            ],
            height: "500px",
            pageable: {
                refresh: true,
                pageSizes: true,
                buttonCount: 5
            },
        }).data("kendoGrid");
    }

</script>  

<?php include("../footer.php"); ?>
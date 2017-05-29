

app.controller("permissionCtrl", function ($scope, permissionService) {

   
   permissionService.get().then(function (response) {
                        
                        console.log(response.data);
                            //console.log(response);

                        },
                        function (err) {
                            $scope.message = err.error_description;
                        });


      var sampleData = [
            {ProductID: 1, ProductName: "Apple iPhone 5s", Introduced: new Date(2013, 8, 10), UnitPrice: 525, Discontinued: false, UnitsInStock: 10},
            {ProductID: 2, ProductName: "HTC One M8", Introduced: new Date(2014, 2, 25), UnitPrice: 425, Discontinued: false, UnitsInStock: 3},
            {ProductID: 3, ProductName: "Nokia 5880", Introduced: new Date(2008, 10, 2), UnitPrice: 275, Discontinued: true, UnitsInStock: 0}
        ];

        // custom logic start

        var sampleDataNextID;

        

        function getIndexById(id) {
            var idx,
                l = sampleData.length;

            for (var j=0; j < l; j++) {
                if (sampleData[j].ProductID == id) {
                    return j;
                }
            }
            return null;
        }

          
          var dataSource = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        // on success
                        e.success(sampleData);
                        // on failure
                        //e.error("XHR response", "status code", "error message");

                        
                    },
                    create: function (e) {
                        // assign an ID to the new item
                        e.data.ProductID = sampleDataNextID++;
                        // save data item to the original datasource
                        sampleData.push(e.data);
                        // on success
                        e.success(e.data);
                        // on failure
                        //e.error("XHR response", "status code", "error message");
                        console.log(e.data);
                    },
                    update: function (e) {
                        // locate item in original datasource and update it
                        sampleData[getIndexById(e.data.ProductID)] = e.data;
                        // on success
                        e.success();
                        // on failure
                        //e.error("XHR response", "status code", "error message");
                    },
                    destroy: function (e) {
                        // locate item in original datasource and remove it
                        sampleData.splice(getIndexById(e.data.ProductID), 1);
                        // on success
                        e.success();
                        // on failure
                        //e.error("XHR response", "status code", "error message");
                    }
                },
                error: function (e) {
                    // handle data operation error
                    alert("Status: " + e.status + "; Error message: " + e.errorThrown);
                },
                pageSize: 10,
                batch: false,
                schema: {
                    model: {
                        id: "ProductID",
                        fields: {
                            ProductID: { editable: false, nullable: true },
                            ProductName: { validation: { required: true } },
                            Introduced: { type: "date" },
                            UnitPrice: { type: "number", validation: { required: true, min: 1} },
                            Discontinued: { type: "boolean" },
                            UnitsInStock: { type: "number", validation: { min: 0, required: true } }
                        }
                    }
                }
            });

            $("#grid").kendoGrid({
                dataSource: dataSource,
                pageable: true,
                toolbar: ["create"],
                columns: [
                    { field: "ProductName", title: "Mobile Phone" },
                    { field: "Introduced", title: "Introduced", format: "{0:yyyy/MM/dd}", width: "200px" },
                    { field: "UnitPrice", title: "Price", format: "{0:c}", width: "120px" },
                    { field: "UnitsInStock", title:"Units In Stock", width: "120px" },
                    { field: "Discontinued", width: "120px" },
                    { command: ["edit", "destroy"], title: "&nbsp;", width: "200px" }
                ],
                editable: "inline"
            });

            
      
     /*
        $scope.login = function () {

            $scope.loginData = {
                Email: $scope.Email,
                Password: $scope.Password
            };

            permissionService.get().then(function (response) {
               
               console.log(response.data);
                //console.log(response);

            },
             function (err) {
                 $scope.message = err.error_description;
             });
        };
      */

});
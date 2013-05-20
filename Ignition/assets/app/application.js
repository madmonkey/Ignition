'use strict';
var ignitionApplication = angular.module('ignitionApplication', []);
ignitionApplication.controller('AuditController', function($scope, $http) {
    $scope.records = [
        { Id: 1, Type: 'I', TableName: 'Something', FieldName: 'Id', FieldType: 'string', OldValue: 'null', NewValue: '1', UpdateDate: '2/14/2013' },
        { Id: 2, Type: 'I', TableName: 'Else', FieldName: 'Value', FieldType: 'string', OldValue: 'null', NewValue: '1', UpdateDate: '2/14/2013' }
    ];
});

ignitionApplication.controller('CompaniesController', function($scope, $http) {
    $scope.data = [{
        "Id": "1",
        "Name": "Publix",
        "Code": "PBLX",
        "Contacts": [{
            "Company": "1",
            "Id": 1,
            "FirstName": 'Andrew',
            "LastName": 'Del Preore',
            "Title": 'VP',
            "Category": 'Preferred'}],
        "ContactDetails": [{
            "ContactType": "phone",
            "ContactValue": "(407) 681-1415"
        }, {
            "ContactType": "fax",
            "ContactValue": "(407) 681-1234"
        }],
        "Addresses": [{ Address: '12 Something', City: 'Orlando', Region: 'Southeast', PostalCode: '32817', Country: 'USA' }] 
    },
        {
            "Id": "2",
            "Name": "Walgreens",
            "Code": "WALGR",
            "Contacts": [{
                "Company": "2",
                "Id": 2,
                "FirstName": 'LaVerne',
                "LastName": 'Del Preore',
                "Title": 'CEO',
                "Category": 'VIP'}],
            "ContactDetails": [{
                "ContactType": "phone",
                "ContactValue": "(407) 681-1111"
            }, {
                "ContactType": "fax",
                "ContactValue": "(407) 681-3333"
            }],
            "Addresses": [{ Address: '43 Manin Str', City: 'Orlando', Region: 'Southeast', PostalCode: '32817', Country: 'USA' }]  
        }
    ];
});
    
    //$scope.companies = [
    //    {
    //        Id: 1, Name: 'Publix', Code: 'PBLX',
    //        Contacts: [{ Id: 1, FirstName: 'Andrew', LastName: 'Del Preore', Title: 'VP', Category: 'Preferred' }],
    //        Addresses: [{ Address: '12 Something', City: 'Orlando', Region: 'Southeast', PostalCode: '32817', Country: 'USA' }],
    //        ContactDetails: [{ ContactType: "phone", ContactValue: "(407) 681-1415" }, { ContactType: "fax", ContactValue: "(407) 681-1234" }]
    //    },
    //    {
    //        Id: 2, Name: 'Walgreens', Code: 'WALGR',
    //        Contacts: [{ Id: 2, FirstName: 'LaVerne', LastName: 'Del Preore', Title: 'CEO', Category: 'VIP' }],
    //        Addresses: [{ Address: '234 Main Street', City: 'Orlando', Region: 'Southeast', PostalCode: '32817', Country: 'USA' }],
    //        ContactDetails: [{ ContactType: "phone", ContactValue: "(407) 681-1111" }, { ContactType: "fax", ContactValue: "(407) 681-2222" }]
    //    }
    //];

//function SearchCtrl($scope, $http, page, limit) {
//    $scope.url = 'audit?pg=' + page + '&limit=' + limit; // The url of our search

//    // The function that will be executed on button click (ng-click="search()")
//    $scope.search = function () {

//        // Create the http post request
//        // the data holds the keywords
//        // The request is a JSON request.
//        //$http.post($scope.url, { "data": $scope.keywords }).
//        $http.get($scope.url).
//        success(function (data, status) {
//            $scope.status = status;
//            $scope.data = data;
//            $scope.result = data; // Show result from server in our <pre></pre> element
//        })
//        .
//        error(function (data, status) {
//            $scope.data = data || "Request failed";
//            $scope.status = status;
//        });
//    };
//}
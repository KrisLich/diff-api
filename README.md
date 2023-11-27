# DiffAPI

 A simple diffing API using ASP.NET Core .NET 8.0

## Introduction

This ASP.NET Core API provides endpoints for comparing data and identifying differences. The API exposes two endpoints to submit data for comparison and a third endpoint to retrieve the comparison results.

### Prerequisites

- .NET 8.0 Runtime

### Installation / Setup

1. Clone the repository.
2. Build and run the solution
3. Open or use an app such as Postman
4. Refer to the provided endpoints

## Usage

### Endpoints

##### 1. Upload Left Data
This endpoint uploads the 1st (left) data to be diffed.
Type: PUT
Endpoint: PUT /v1/diff/{id}/left
Parameters:
-- id (string): identifier for the data
Request Body:
{
    "data": "YourFirstDataToBeDiffed"
}
Response:
201 Created - if the data is successfully uploaded

##### 2. Upload Right Data
This endpoint uploads the 2nd (right) data to be diffed.
Type: PUT
Endpoint: PUT /v1/diff/{id}/right
Parameters:
-- id (string): identifier for the data
Request Body:
{
    "data": "YourSecondDataToBeDiffed"
}
Response:
201 Created - if the data is successfully uploaded

##### 3. Get Diff Results
This endpoint retrieves the diff results for the specified data.
Type: GET
Endpoint: GET /v1/diff/{id}
Parameters:
-- id (string): identifier for the data
Response:
200 Ok - with JSON containing "diffResultType" and "diffs"
value of diffResultType: 
-- if the data is equal: "Equals"
-- if the data is different: "SizeDoNotMatch"
-- if the content does not match: "ContentDoNotMatch"
value of diff:
diff will only have value if size matches, with values of "offset" and "length"
value of offset: on which index the change occurs
value of length: for how long there is a change
there can be multiple separate changes

### Example

1. Uploading to left data for ID 1 with data "AAAAAA=="
PUT /v1/diff/1/left
```json
{
"data": "AAAAAA=="
}
```
Response: 201 Created

2. Uploading to right data for ID 1 with data "AAAAAA=="
PUT /v1/diff/1/right
```json
{
"data": "AAAAAA=="
}
```
Response: 201 Created

3. Getting diff results for ID 1
GET /v1/diff/1
```json
{
    "diffResultType": "ContentDoNotMatch",
    "diffs": [
        {
            "offset": 1,
            "length": 1
        },
        {
            "offset": 3,
            "length": 1
        },
        {
            "offset": 5,
            "length": 1
        }
    ]
}
```
Response: 200 OK

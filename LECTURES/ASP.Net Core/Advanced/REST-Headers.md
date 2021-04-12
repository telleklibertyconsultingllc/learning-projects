# RESTful Verbs
- GET => returns a response
- HEAD => is identical to GET, with the notable difference that the API shouldn't return a response body
- PUT => updates a resource
- PATCH => like PUT, updates part of a resource
- POST => creates a resource
- DELETE => deletes a resource
- OPTIONS => tells the consumer what verbs are allowed on a RESTful API

## Passing Data to the API

- FromBody = Request Body. Inferred for complex types.
- FromForm = Form data in the request Body. Inferred for action parameters of type IFormFile and IFormFileCollection
- FromHeader = Request Header.
- FromQuery = Query String parameters. Inferred for any other action parameters.
- FromRoute = Route data from the current request. Inferred for any action parameter name matching a parameter in the route template
- FromService = The service injected as action parameter

- Once passed without an attribute, it is assumed the complex type is passed from the body.
IActionResult<IEnumerable<Response>> GetData([FromQuery] AuthorRequest request)

## Filtering and Searching
- Filtering is exact
- Searching is widening your search criteria
- Are passed as query parameters or requests
-- Always use a Request or DTO when building the model and call to get data from the database
-- UI may be query parameters; however, the query parameter can accept a certain length

## Deferred Execution
- Query execution occurs sometime after the query is constructed
- IQueryable<T>: creates an expression tree.
-- foreach loop
-- ToList, ToArray, ToDictionary
-- Singleton Queries

## Creating Resources
- Method safety and method idempotency
-- Method Safety => is considered safe when it doesn't change the resource respresentation
-- Method Idempotency => is considered idempotent when it can be called multiple times with the same result
--- GET/OPTIONS/HEAD Safe/Idempotent
--- POST NOT SAFE/NOT IDEMPOTENT
--- DELETE NOT SAFE/IDEMPOTENT
--- PUT NOT SAFE/IDEMPOTENT
--- PATCH NOT SAFE/NOT IDEMPOTENT
- Create Resources
-- Status Code
-- Sending a list of Resources
- Supporting additional content-type value and input formatters
- Supporting OPTIONS

## Checking Validation Rules
- ModelState => a dictionary containing both the state of the model and model binding validation
- ModelStats.IsValid() => determine if model is validated

## Reporting Validation Errors
- 422 Response status =? Unprocessable entity
- Response body should contain validation errors

- If property validation fails, class validation may never occur

## Customizing Error Messages
- ErrorMessage Property on Attributes

## Updating Resources
- PUT for full updates
- PATCH for partial updates
-- Allows sending over change sets via JsonPatchDocument

## Upserting
- Creating a resource with PUT or PATCH

## Http Method Overview by Use Course
- GET api/authors
200 [author, author], 404 Not Found
- GET api/authors/{authorId}
200 {author}, 404 Not Found
- DELETE api/authors/{authorId}
204, 404
- DELETE api/authors (Bad Design, Don't delete collection of child resource)
204, 404
Rarely implemented

Creating resources (server)
- POST api/authors {author}
201 {author}, 404
- POST api/author/{authorId} can never be successful (404 or 409)

Create a new resource for adding a collection in one go
- POST api/authorcollections - {authorCollection}
201 {authorCollection}, 404

Creating resources (consumer)
- PUT api/authors/{authorid} - {author}
201 {author}
NOT FOUND will create the author

- PATCH api/authors/{authorId} - {JsonPatchDocument on author}
201 {author}

- Updating resources (full)
- PUT api/authors/{author} - {author}
200 {author}, 204, 404
- PUT api/authors - [{author}, {author}]
200 [{author}, {author}], 204, 404
Rarely Implemented

- Updating resources (partial)
- PATCH api/authors/{authorId} - {JsonPatchDocument on author}
200 {author}, 204, 404
- PATCH api/authors - {JsonPatchDocument}
200 [{author}, {author}], 204, 404
Rarely implemented

## List of Patch Operations
- JSON Patch Operations
1. Add
op - add
path = /a/b => property path
value = test
2. Remove
op - remove
path
value
3. Replace
op - replace
path
value
4. Copy
op - copy
path
value
5. Move
op - move
path
value
6. Test => Checks if a property exist in a specific property path
op - test
path
value

- Header Information
Content-Type => application/json-patch+json
Accept       => application/json

raw in POSTMAN
[
    {
        "op": "replace",
        "path": "/title",
        "value": "Updated Title"
    }
]

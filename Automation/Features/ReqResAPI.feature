Feature: ReqResAPI

	Scenario:  Successful Registration
	Given username "eve.holt@reqres.in" and password "pistol" as inputs
	When the register api call is made using POST
	Then the response code should be 200  and the token should be "QpwL5tke4Pnpja7X4"

	Scenario:  Unsuccessful Registration using wrong credentials
	Given username "eve.holt2222@reqres.in" and password "wrongpassw22ord" as inputs
	When the register api call is made using POST
	Then the response code should be 400 and error "Only defined users succeed registration" is returned

	Scenario:  Return all users
	Given parameter "per_page=100" is included in the header
	When the users api call is made using GET
	Then the response code should be 200

	Scenario:  Return all users and verify exact amount
	Given parameter "per_page=100" is included in the header
	When the users api call is made using GET
	Then the total amount of users should be 12
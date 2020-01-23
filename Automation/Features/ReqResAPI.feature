Feature: ReqResAPI

	Scenario:  Successful Registration
	Given username "eve.holt@reqres.in" and password "pistol" as inputs
	When the register api call is made using POST
	Then the response code should be 200

	Scenario:  Unsuccessful Registration
	Given username "eve.holt@reqres.in" and password "wrongpassword" as inputs
	When the register api call is made using POST
	Then the response code should be 400

	Scenario:  Return all users
	Given parameter "per_page=100" is included in the header
	When the users api call is made using GET
	Then the response code should be 200 and a list of users is returned
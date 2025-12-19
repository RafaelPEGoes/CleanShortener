Feature: 3 - Backgrounds

As an engineer
I want to show how to use Backgrounds
So that the common pre-conditions are abstracted away from the specification

Background: 
	Given the url "https://www.google.com"
	When I request a shortened url
	Then I receive a shortened url in response

@CreateShortUrl
Scenario: Shortened url should redirect user to the original url
	Given the shortened url bound to "https://www.google.com"
	When I open the shortened url
	Then I should be redirected to the original url
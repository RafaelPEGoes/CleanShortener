Feature: Create Short Url

As an user of CleanShortener
I want the capability of get shorter versions of regular urls
So I comply with internal business requirements and constraints

@tag1
Scenario: A long url is reduced to a smaller one
	Given the url "https://www.google.com/search?q=how+to+bdd"
	When I request a shortened url
	Then I receive a shortened url in response

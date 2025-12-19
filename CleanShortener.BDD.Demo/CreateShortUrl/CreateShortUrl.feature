Feature: Create Short Url

As an user of CleanShortener
I want the capability of get shorter versions of regular urls
So I comply with internal business requirements and constraints

@tag1
Scenario: Shortened url redirects to another address when accessed
	Given the url "https://www.google.com"
	When I request a shortened url
	Then I receive a shortened url in response

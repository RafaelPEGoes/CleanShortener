Feature: 2 - Narratives

As a scrum team member
I want to show the importance of the Narrative Section
To create well documented specifications inteligible by any reader

@DeleteShortUrl
Scenario: Shortened urls should be invalidated after deletion
	
	As a user of CleanShortener
	I want the capability of deleting short links I've previously created
	So they can not be used when they're no longer necessary

	Given the url "https://www.google.com"
	When I request a shortened url
	Then I receive a shortened url in response
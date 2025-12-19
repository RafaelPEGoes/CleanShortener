Feature: Get Short Url

As an user of CleanShortener
I want shortened urls that I previously created
To redirect me to their original url

Background: 
	Given the url "https://www.google.com"
	When I request a shortened url
	Then I receive a shortened url in response

@CreateShortUrl
Scenario: Shortened url should redirect user to the original url
	Given the shortened url bound to "https://www.google.com"
	When I open the shortened url
	Then I should be redirected to the original url

Feature: 1 - ScenarioAndOutlines

As an engineer
I want to demonstrate what are Scenarios and ScenarioOutlines
So the tech talk participants get to know when to use each

@Scenario @SingleTest
Scenario: This is a Scenario to create a short url
	Given the url "https://www.google.com/search?q=scenario+vs+scenario+outline+reqnroll"
	When I request a shortened url
	Then I receive a shortened url in response

@ScenarioOutline @MultipleTests @ParameterizedTests
Scenario Outline: Any url is accepted as long as its HTTP and HTTPS
	Given the url "<Url>"
	When I open the shortened url
	Then I receive a shortened url in response

Examples:
	| Url                                                                   |
	| http://dinosaurs.conspiracy-theories.com                              |
	| https://www.google.com/search?q=scenario+vs+scenario+outline+reqnroll |
	| http://salem.lib.virginia.edu                                         |

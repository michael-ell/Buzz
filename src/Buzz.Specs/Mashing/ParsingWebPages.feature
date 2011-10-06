Feature: Parsing a Web Page
	In order to determine if words are buzzwords
	As an awesome app
	I want to be able to break a web page down into words

Scenario: A page that exists
	Given I have a valid url
	When I parse the content
	Then raw words should be returned

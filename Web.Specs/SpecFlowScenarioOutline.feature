@web
Feature: Scenario Outline Feature

    @active
    Scenario Outline: Navigation Tests
	    Given I navigate to '<url>'
		Then the page title is '<title>'

		    Examples:
		    | name   | url                   | title |
			| Google | http://www.google.com | Google|
			    | Yahoo  | http://www.yahoo.com  | Yahoo |

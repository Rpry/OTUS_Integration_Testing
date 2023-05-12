Feature: Course
	Base Course operations

@mytag
Scenario: Create new course
	Given new course is created 		
	When the course is being searched
	Then the course should be found
	
	
Scenario: Create new lesson
	Given new lesson is created 		
	When the lesson is being searched
	Then the lesson should be found
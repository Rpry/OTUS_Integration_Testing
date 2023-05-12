Feature: Course
	Base Course operations

@mytag
Scenario: Create new course
	Given new course is created 		
	When the course is being searched
	Then the course should be found
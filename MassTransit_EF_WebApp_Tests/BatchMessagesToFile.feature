Feature: MessageProcessingFeature
	Simple batching of messages from a queue


Scenario: Save Single Message
	Given there is a message on the queue
	When the message is processed
	Then the message should be saved to the database
	And a file with the single message should be created
	
Scenario: Save Many Messages
	Given there are 100 messages on the queue
	When the messages are processed
	Then the messages should be saved to the database
	And a file with all messages should be created
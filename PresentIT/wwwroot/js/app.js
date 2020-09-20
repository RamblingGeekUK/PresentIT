// replace these values with those generated in your TokBox Account
var apiKey = "46926164";
var sessionId = "2_MX40NjkyNjE2NH5-MTYwMDU1MDM4NzAwNX44TWtsTG82U1VDWGx2MnJiSU9RUGV0OUl-fg";
var token = "T1==cGFydG5lcl9pZD00NjkyNjE2NCZzaWc9NDRkNDFjYzJhNDRiNTkxMzcwZTQwNWZmNmI2M2Y3MGViMjk4ZTk1YTpzZXNzaW9uX2lkPTJfTVg0ME5qa3lOakUyTkg1LU1UWXdNRFUxTURNNE56QXdOWDQ0VFd0c1RHODJVMVZEV0d4Mk1uSmlTVTlSVUdWME9VbC1mZyZjcmVhdGVfdGltZT0xNjAwNTUwNDAwJm5vbmNlPTAuNTQzNjQzMDAxNzE2NTU4MSZyb2xlPXB1Ymxpc2hlciZleHBpcmVfdGltZT0xNjAzMTQyMzk5JmluaXRpYWxfbGF5b3V0X2NsYXNzX2xpc3Q9";

// (optional) add server code here
initializeSession();

// Handling all of our errors here by alerting them
function handleError(error) {
    if (error) {
        alert(error.message);
    }
}

function initializeSession() {
    var session = OT.initSession(apiKey, sessionId);

    // Create a publisher
    var publisher = OT.initPublisher('publisher', {
        insertMode: 'append',
        width: '100%',
        height: '100%',
    }, handleError);

    // Connect to the session
    session.connect(token, function (error) {
        // If the connection is successful, publish to the session
        if (error) {
            handleError(error);
        } else {
            session.publish(publisher, handleError);
        }
    });
}

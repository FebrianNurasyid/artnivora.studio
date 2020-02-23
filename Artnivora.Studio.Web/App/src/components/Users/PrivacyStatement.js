import React, { Component } from 'react';

class PrivacyStatement extends Component {
    constructor(props) {
        super(props);
    }
    render() {
        return (
            <span>
                <div className="container">
                    <h2> Privacy & Cookie Policy </h2>
                    <hr className="style14" />

                    <div className="entry">
                        <p>Your privacy is very important to us. Accordingly, we have developed this Policy in order for you to understand how we collect, use, communicate and disclose and make use of personal information. The following outlines our privacy policy.</p>
                        <ul>
                            <li>Before or at the time of collecting personal information, we will identify the purposes for which information is being collected.</li>
                            <li>We will collect and use of personal information solely with the objective of fulfilling those purposes specified by us and for other compatible purposes,
                                unless we obtain the consent of the individual concerned or as required by law.</li>
                            <li>We will only retain personal information as long as necessary for the fulfillment of those purposes.</li>
                            <li>We will collect personal information by lawful and fair means and, where appropriate, with the knowledge or consent of the individual concerned.</li>
                            <li>Personal data should be relevant to the purposes for which it is to be used, and, to the extent necessary for those purposes,
                                should be accurate, complete, and up-to-date.</li>
                            <li>We will protect personal information by reasonable security safeguards against loss or theft, as well as unauthorized access, disclosure, copying, use or modification.</li>
                            <li>We will make readily available to customers information about our policies and practices relating to the management of personal information.</li>
                        </ul>
                        <p>We are committed to conducting our business in accordance with these principles in order to ensure that the confidentiality of personal information is protected and maintained.</p>
                    </div>
                    <hr className="style14" />

                    <h3>What personal data we collect and why we collect it</h3>
                    <h4>1. Form</h4>
                    <ul>
                        <li>When ordering or registering on our Site you may be asked to enter your name, member name, email address, mailing address, country,
                            billing information or other details to help you with your experience. These information are collected in purpose of providing services described on it,
                            like to verify your identity when you sign in to website, to process your transactions made on site, to respond to support tickets and offer customer services,
                            for administrative and accounting needs that we required to provide to government.</li>
                        <li>When you submit a support question we collect your first name, last name and your email address so that we can correspond with you.</li>
                    </ul>
                    <h4>2. Google Analytics</h4>
                    <ul>
                        <li>We use Google Analytics to track visitors on this site. Google Analytics uses cookies to collect this data.
                            In order to be compliant with the new regulation Google included a data processing amendment.
                            The data we collect will be processed anonymously and “Data sharing” is disabled. We don’t use other Google services in combination with Google Analytics cookies.</li>
                    </ul>
                    <hr className="style14" />

                    <h3>Comments</h3>
                    <p>When visitors leave comments on the site we collect the data shown in the comments form, and also the visitor’s IP address and browser user agent string to help spam detection.</p>
                    <p>An anonymized string created from your email address (also called a hash) may be provided to the Gravatar service to see if you are using it. The Gravatar service privacy policy is available here: https://automattic.com/privacy/. After approval of your comment, your profile picture is visible to the public in the context of your comment.</p>
                    <hr className="style14" />
                    <h3>Cookie Policy</h3>
                    <p>To enhance your experience on our sites, many of our web pages use cookies. Cookies are small text files that we place in your computer’s browser to store your preferences.
                        Cookies, by themselves, do not tell us your email address or other personal information unless you choose to provide this information to us by,
                        for example, registering at one of our sites. Once you choose to provide a web page with personal information, this information may be linked to the data stored in the cookie. A cookie is like an identification card.
                        It is unique to your computer and can only be read by the server that gave it to you.
                        </p>
                    <p>We use cookies to understand site usage and to improve the content and offerings on our sites. For example, we may use cookies to personalise your experience on our web pages (e.g. to recognise you by name when you return to our site). We also may use cookies to offer you products and services.
                        <br /> If you leave a comment on our site you may opt-in to saving your name, email address and website in cookies. These are for your convenience so that you do not have to fill in your details again when you leave another comment. These cookies will last for one year.
                        <br /> If you have an account and you log in to this site, we will set a temporary cookie to determine if your browser accepts cookies. This cookie contains no personal data and is discarded when you close your browser.
                        <br /> When you log in, we will also set up several cookies to save your login information and your screen display choices. Login cookies last for two days, and screen options cookies last for a year. If you select Remember Me, your login will persist for two weeks. If you log out of your account, the login cookies will be removed.
                        </p>
                    <p>If you want to control which cookies you accept. You can configure your browser to accept all cookies or to alert you every time a cookie is offered by a website’s server.
                        Most browsers automatically accept cookies. You can set your browser option so that you will not receive cookies and you can also delete existing cookies
                        from your browser. You may find that some parts of the site will not function properly if you have refused cookies.
                        </p>
                    <hr className="style14" />

                    <h3>Embedded content from other websites</h3>
                    <p>Articles on this site may include embedded content (e.g. videos, images, articles, etc.). Embedded content from other websites behaves in the exact same way as if the visitor has visited the other website.<br /> These websites may collect data about you, use cookies, embed additional third-party tracking, and monitor your interaction with that embedded content, including tracing your interaction with the embedded content if you have an account and are logged in to that website.</p>
                    <hr className="style14" />

                    <h3>Security</h3>
                    <p>To protect your personal information, we take reasonable precautions and follow industry best practices to make sure it is not inappropriately lost, misused, accessed, disclosed, altered or destroyed.<br /> If you provide us with your credit card information, the information is encrypted using secure socket layer technology (SSL) and stored with a AES-256 encryption. Although no method of transmission over the Internet or electronic storage is 100% secure, we follow all PCI-DSS requirements and implement additional generally accepted industry standards.</p>
                </div>
            </span>
        );
    }
}

export default (PrivacyStatement);
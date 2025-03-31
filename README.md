# Math Jungle - Implementation Documentation

## Overview
Math Jungle is a math learning application designed for children aged 5-8 with dyscalculia. The app provides an engaging and interactive environment to help children improve their mathematical skills through gamified exercises and challenges.

## Understanding Dyscalculia
Dyscalculia is a specific learning difficulty that affects a child’s ability to understand, learn, and perform math-related tasks. Children with dyscalculia often experience:
- **Difficulty with Number Sense**: Struggling to understand quantities and how numbers relate to each other.
- **Challenges in Arithmetic Operations**: Basic operations like addition or subtraction can be confusing.
- **Math Anxiety**: Frustration and stress around math can hinder learning.
- **Time Management and Sequencing Issues**: Organizing and sequencing numbers or steps can be challenging.


## How It Works
Math Jungle is structured around a fun and interactive journey through different levels of math challenges. The app adapts to each child's learning pace and provides personalized feedback.

### Game Flow
1. **User Profile Creation**: The child or parent sets up a profile with customizable avatars.
2. **Level Selection**: The child selects or is assigned a level based on previous performance.
3. **Gameplay & Exercises**: The child plays mini-games and solves math problems tailored to their ability.
4. **Rewards & Feedback**: The app provides instant feedback, rewards (badges, stars), and progress tracking.
5. **Parental Insights**: Parents and educators can view performance reports and recommendations.


### Core Games
Each game is designed to address specific challenges faced by children with dyscalculia.

#### **Game 1: Number Sequencing with Apple Trees**
- Helps children recognize and memorize the order of numbers.
- Encourages step-by-step sequencing, reinforcing time management skills.
- Provides gentle error correction to build confidence.

#### **Game 2: Counting with Banana Trees**
- Strengthens number sense by linking numerical symbols to actual quantities.
- Improves arithmetic skills through hands-on counting.
- Reduces math anxiety by creating a stress-free learning environment.

#### **Game 3: Category-Based Selection with Mixed Apple Trees**
- Teaches differentiation between categories (e.g., color sorting).
- Develops multi-step sequencing and executive functioning skills.
- Reinforces accuracy through guided error correction.


## Features
- **Personalized Learning**: Adapts to the child’s strengths and weaknesses.
- **Interactive Exercises**: Includes puzzles, quizzes, and visual learning tools.
- **Gamified Rewards System**: Encourages learning through achievements and unlockable content.
- **Child-Friendly Interface**: Simple navigation, colorful design, and audio instructions.
- **Multi-Platform Support**: Available on Web, Android, and iOS.
- **Offline Mode**: Certain features are accessible without an internet connection.
- **Parental Dashboard**: Provides analytics on child progress and areas needing improvement.


## Technologies Used
- **Frontend**:  
  - **Unity (C#)** – Used for building the game-based UI/UX, animations, and interactive components.

- **Backend**:  
  - **Spring Boot (Java)** – Provides a robust and scalable framework for handling RESTful API requests and business logic.
  - **Spring Security with JWT** – Handles user authentication and session management securely.

- **Database**:  
  - **MySQL** – Manages structured data such as user progress, game data, and other records.

- **Version Control**:  
  - **Git & GitHub** – For collaborative development and maintaining version history.

- **Deployment/CI-CD Pipeline**:  
  - **GitHub Actions** – Automates the build, test, and deployment process.
  - **Google Cloud Platform (GCP)** – Utilizes services like Cloud Run (for containerized application deployment), Artifact Registry (for storing Docker images), and Cloud SQL (for hosting the MySQL database).


## Contributors
- Nipuna Rajapakse (Agent NR)
- Dasun Tharanga
- Koojana Nishagi (KoojanaN)
- Aloka Pathiraja (webdevpathiraja)
- Chanuri Pathirana (dchanuri)
- Prasansa Ramidu


## License
This project is licensed under the MIT License. See the LICENSE file for details.


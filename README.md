# PersonalBlog

A personal blog project built using ASP.NET Core MVC. This application features the basic CRUD operations, allowing the user to create, view, edit or delete blog articles; and uses authentication to control access to the pages.

Based on: https://roadmap.sh/projects/personal-blog

## Features

The blog consists of two sections: a guest section and an admin section.

**Guest Section** — A list of pages that can be accessed by anyone:

- **Home Page**: This page will display the list of articles published on the blog. This page also includes a search bar and a category filter.
- **Article Page**: This page will display the content of the article alongside data such as publish date, date of last modification, categories and tags.

**Admin Section** — are the pages that only you can access to publish, edit, or delete articles.

- **Dashboard**: This page will display the list of articles published on the blog along with the option to add a new article, edit an existing article, or delete an article. This page also includes a search bar and a category filter.
- **Create Article Page**: This page will contain a form to add a new article. The form will have fields like filename, title, content, summary, categories and tags.
- **Edit Article Page**: This page will contain a form to edit an existing article. The form will have fields like title, content, summary, categories and tags.

### Articles

Each article it's a markdown document that contains it's own metadata as YAML frontmatter. The frontmatter values an article may contain are: title, summary, date of publication, date of last modification, tags and categories.

### Storage

Articles are stored as separate files in the filesystem. The project already comes with several sample articles.

### Authentication

The application uses cookie authentication to handle login into the admin section. You can use any of these credentials to sign in:

- Username: **_Admin_** <br>
  Password: **_password_**
- Username: **_UserNumber2_** <br>
  Password: **_password_**
- Username: **_TotallyRealUser33_** <br>
  Password: **_password_**

## Images

![Home page](/preview/home_page.png 'Home page')
![Article page](/preview/article_page.png 'Article page')
![Login page](/preview/login.png 'Login page')
![Admin dashboard](/preview/admin_dashboard.png 'Admin dashboard')
![Create page](/preview/create_page.png 'Create page')
![Edit page](/preview/edit_page.png 'Edit page')
![Delete page](/preview/delete_page.png 'Delete page')

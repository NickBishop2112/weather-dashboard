interface LayoutProps {
    children: React.ReactNode;
}

export const Layout:React.FC<LayoutProps> = ({ children }) => {
    return (
        <div className="min-h-screen flex flex-col bg-gray-50 text-gray-800">
            {/* Move h1 to the top of the page */}
            <header className="bg-gray-700 text-white p-5 shadow-md">
                <div className="container mx-auto flex justify-between items-center">
                    <h2 className="text-lg font-medium tracking-wide">
                        Weather Dashboard
                    </h2>
                </div>
            </header>

            {/* Main Content */}
            <main className="flex-1 container mx-auto p-6 max-w-4xl">{children}</main>

            {/* Footer */}
            <footer className="bg-gray-700 text-white p-4 text-center shadow-md mt-6">
                <p className="text-sm opacity-80">
                    © {new Date().getFullYear()} Weather Dashboard. All rights reserved.
                </p>
            </footer>
        </div>
    );    
};